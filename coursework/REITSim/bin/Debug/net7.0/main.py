from tkinter import *
from tkinter import messagebox as msg
from GUI.PageManager import *
from GUI.GameManager import GameManager
from functools import partial

# global settings

GS = {
    "title": "REIT Simulator",
    "width": 800,
    "height": 600,
    "resizable_W": False,
    "resizable_H": False,
}

# create and configure window

tk = Tk()
tk.title(GS["title"])
tk.geometry(f"{GS['width']}x{GS['height']}")
tk.resizable(GS["resizable_W"], GS["resizable_H"])

# create page manager and game variable

PM = PageManager(tk, ["main_menu", "new_game", "load_game", "main_game_menu", "cities", "property", "clients"])
GAME: GameManager or None = None

# define functions, add pages content and available commands


def new_game(name) -> None:
    global GAME
    if name:
        GAME = GameManager(name)

        PM.switch("main_game_menu")
        update_main_page()


def update_main_page() -> None:
    GAME.update_income()
    info = GAME.get_player_stats()
    mediator = PM.pages["main_game_menu"]

    mediator.mediate(0, 0, 0, info[0])
    mediator.mediate(0, 1, 0, f"Reputation: {info[1]}")
    mediator.mediate(0, 2, 0, f"Finances: {info[2]}")
    mediator.mediate(0, 3, 0, f"Shares available: {info[3]}" + (f" ({info[5]} on exchange)" if info[5] != 0.0 else ""))
    mediator.mediate(0, 4, 0, f"Share price: {info[4]}")
    mediator.mediate(0, 6, 0, f"Turn {GAME.turn}")
    mediator.mediate(0, 7, 0, f"+{info[6]}" + (f" (+exchange)" if info[5] != 0.0 else ""))

    mediator.on(1, 0)
    mediator.on(1, 2)

    mediator.off(0, 5)
    mediator.off(1, 1)
    mediator.off(1, 3)
    mediator.off(1, 5)
    mediator.off(2, 0)


def open_sell_shares() -> None:
    mediator = PM.pages["main_game_menu"]

    mediator.off(1, 0)
    mediator.off(1, 2)

    mediator.on(0, 5)
    mediator.on(1, 1)
    mediator.on(1, 5)
    mediator.on(2, 0)


def sell_shares() -> None:
    mediator = PM.pages["main_game_menu"]

    GAME.sell_shares(mediator.mediate(2, 0, 0))
    mediator.mediate(2, 0, 1, "")
    update_main_page()


def open_buy_shares() -> None:
    mediator = PM.pages["main_game_menu"]

    mediator.off(1, 0)
    mediator.off(1, 2)

    mediator.on(0, 5)
    mediator.on(1, 3)
    mediator.on(1, 5)
    mediator.on(2, 0)


def buy_shares() -> None:
    mediator = PM.pages["main_game_menu"]

    GAME.buy_shares(mediator.mediate(2, 0, 0))
    mediator.mediate(2, 0, 1, "")
    update_main_page()


def cancel_buying() -> None:
    mediator = PM.pages["main_game_menu"]

    mediator.mediate(2, 0, 1, "")
    update_main_page()


def on_main_page() -> None:
    PM.switch("main_game_menu")
    update_main_page()


def on_cities_page() -> None:
    PM.switch("cities")

    mediator = PM.pages["cities"]

    mediator.mediate(0, 1, 0, "Taxation: 0.0")
    mediator.mediate(0, 3, 0, f"Finances: {GAME.get_player_stats()[2]}")

    names = []
    for city in GAME.get_cities():
        city.Sort()
        names.append(city.Name)

    mediator.mediate(2, 0, 0, names)

    for i in range(4):
        mediator.mediate(3, i, 1)
        mediator.mediate(3, i, 3)


def expand() -> None:
    mediator = PM.pages["cities"]
    GAME.expand()

    names = []
    for city in GAME.get_cities():
        names.append(city.Name)

    mediator.mediate(2, 0, 0, names)
    mediator.mediate(0, 3, 0, f"Finances: {GAME.get_player_stats()[2]}")


def display_city(value) -> None:
    mediator = PM.pages["cities"]

    city = GAME.get_cities()[mediator.mediate(2, 0, 1).index(value)]

    mediator.mediate(0, 1, 0, f"Taxation: {city.Taxation}")

    for i in range(4):
        mediator.mediate(3, i, 3)
        mediator.mediate(3, i, 4, None)

    infos = []

    for i, land in enumerate(city):
        info = []
        info.append(f"Land cost: {land.LandCost}\n")
        info.append(f"Land taxation: {land.LandTax}\n")
        info.append(f"Land size: {land.Size}\n\n")

        if land.HaveBuilding:
            info.append(f"Building: {land.Building.Requirement.Type}\n")
            info.append(f"Of size: {land.Building.Requirement.Size}\n")
            info.append(f"Maintenance: {land.Building.Maintenance}\n")
            info.append(f"Profit: {land.Building.Profit}\n\n")

        if land.PlayerProperty:
            info.append("You have this land")
        else:
            mediator.mediate(3, i, 2)
            mediator.mediate(3, i, 4, partial(on_buying_land, land, mediator, i))

        infos.append(info)

    for i in range(4):
        mediator.mediate(3, i, 1)

    for i, info in enumerate(infos):
        mediator.mediate(3, i, 0, info)


def on_buying_land(land, mediator, i) -> None:
    mediator.mediate(3, i, 3)
    GAME.buy_land(land)
    mediator.mediate(0, 3, 0, f"Finances: {GAME.get_player_stats()[2]}")


def on_property_page() -> None:
    PM.switch("property")

    mediator = PM.pages["property"]

    mediator.mediate(0, 1, 0, f"Finances: {GAME.get_player_stats()[2]}")

    names = []
    for land in GAME.get_property():
        if land.HaveBuilding:
            names.append(f"{land.ParentCity.Name} {land.Building.Requirement.Type} {land.Building.Requirement.Size}")
        else:
            names.append(f"{land.ParentCity.Name} empty")

    if not names:
        names.append("None")

    mediator.mediate(2, 0, 0, names)
    mediator.mediate(3, 0, 1)

    mediator.off(1, 1)
    mediator.off(1, 2)
    mediator.off(1, 3)
    mediator.off(1, 4)
    mediator.off(2, 1)
    mediator.off(2, 2)


def display_land(value) -> None:
    if value == "None":
        return

    mediator = PM.pages["property"]
    mediator.off(1, 4)
    mediator.mediate(2, 0, 3, value)

    land = GAME.get_property()[mediator.mediate(2, 0, 1).index(value)]

    info = [
        f"City: {land.ParentCity.Name} (tax: {land.ParentCity.Taxation})\n"
        f"Size: {land.Size}\n\n"
    ]
    if land.HaveBuilding:
        info.append(f"{land.Building.Requirement.Type} of size {land.Building.Requirement.Size}\n")
        info.append(f"Maintenance: {land.Building.Maintenance}\n")
        info.append(f"Profit: {land.Building.Profit}\n")
        if land.Building.Occupied:
            info.append(f"Holder: {land.Building.Holder.Name}\n")
            info.append(f"Rent for {land.Building.RentFor} turns\n")
        else:
            info.append("Holder: -\n")
        info.append(f"Extend rent? {'Yes' if land.Building.AutoExtention else 'No'}\n\n")
        info.append(f"Raze cost: {land.Building.RazeCost}")
    else:
        info.append("Empty\n")

    mediator.mediate(3, 0, 0, info)

    if land.HaveBuilding:
        mediator.on(1, 1)
        mediator.mediate(1, 1, 0, {"command": lambda: switch_autoextend(land)})

        if not land.Building.Occupied:
            mediator.on(1, 2)
            mediator.mediate(1, 2, 0, {"command": lambda: raze_building(land)})

            mediator.on(2, 1)

            clients = []
            for client in GAME.get_clients():
                if not client.IsHolder and client.Requirement == land.Building.Requirement:
                    clients.append(client.Name)

            if not clients:
                clients.append("None")
            mediator.mediate(2, 1, 0, clients)
    else:
        mediator.on(2, 2)

        info = [
            *[f"Factory {i}" for i in range(1, land.Size + 1)],
            *[f"Shop {i}" for i in range(1, land.Size + 1)],
            *[f"Warehouse {i}" for i in range(1, land.Size + 1)],
            *[f"Office {i}" for i in range(1, land.Size + 1)],
            *[f"ShoppingCentre {i}" for i in range(1, land.Size + 1)],
        ]

        mediator.mediate(2, 2, 0, info)


def display_building(value) -> None:
    mediator = PM.pages["property"]

    land = GAME.get_property()[mediator.mediate(2, 0, 1).index(mediator.mediate(2, 0, 2))]
    building = GAME.new_building(*value.split(), land)

    mediator.on(1, 4)
    mediator.mediate(1, 4, 0, {"text": f"Buy: {building.BuildCost}$", "command": lambda: build(land, building)})


def build(land, building) -> None:
    GAME.build(land, building)
    on_property_page()
    display_land(f"{land.ParentCity.Name} {f'{land.Building.Requirement.Type} {land.Building.Requirement.Size}' if land.HaveBuilding else 'empty'}")


def display_client(value) -> None:
    if value == "None":
        return

    mediator = PM.pages["property"]

    land = GAME.get_property()[mediator.mediate(2, 0, 1).index(mediator.mediate(2, 0, 2))]
    client = GAME.get_clients()[mediator.mediate(2, 1, 1).index(value)]

    mediator.on(1, 3)
    mediator.mediate(1, 3, 0, {"command": lambda: rent_out(land, client)})


def rent_out(land, client) -> None:
    GAME.rent_out(land, client)

    on_property_page()
    display_land(f"{land.ParentCity.Name} {f'{land.Building.Requirement.Type} {land.Building.Requirement.Size}' if land.HaveBuilding else 'empty'}")


def switch_autoextend(land) -> None:
    if land.Building.AutoExtention:
        land.Building.AutoExtention = False
    else:
        land.Building.AutoExtention = True

    on_property_page()
    display_land(f"{land.ParentCity.Name} {f'{land.Building.Requirement.Type} {land.Building.Requirement.Size}' if land.HaveBuilding else 'empty'}")


def raze_building(land) -> None:
    GAME.raze_building(land)
    on_property_page()
    display_land(f"{land.ParentCity.Name} {f'{land.Building.Requirement.Type} {land.Building.Requirement.Size}' if land.HaveBuilding else 'empty'}")


def on_client_page() -> None:
    PM.switch("clients")
    mediator = PM.pages["clients"]

    info = []

    GAME.get_clients().Sort()
    for client in GAME.get_clients():
        info.append(f"{client.Name}\n")
        if not client.IsHolder:
            info.append(f"Requires: {client.Requirement.Type} of size {client.Requirement.Size}\n\n")
        else:
            info.append("already rents property\n\n")

    mediator.mediate(2, 0, 0, info)


def next_turn() -> None:
    global GAME
    GAME.next_turn()
    update_main_page()
    if GAME.get_player_stats()[2] <= 0.0:
        msg.showinfo("Bankrupt", "You have no money.\nThe game is over")
        GAME = None
        PM.switch("main_menu")


def on_closing() -> None:
    if msg.askyesno("Quit", "Are you sure?"):
        tk.destroy()
    else:
        return


pages = {
    "main_menu": [
        [
            SimplifiedLabel(tk, {"text": "REIT Simulator", "font": ("Comic Sans MS", 60)},
                            {"relx": 0.5, "rely": 0.05, "anchor": "n"}),
        ],
        [
            SimplifiedButton(tk, {"text": "New Game", "font": ("Comic Sans MS", 40), "command": lambda: PM.switch("new_game")},
                             {"relx": 0.5, "rely": 0.3, "anchor": "n", "relwidth": 0.4, "relheight": 0.2}),

            SimplifiedButton(tk, {"text": "Load Game", "font": ("Comic Sans MS", 40), "command": lambda: PM.switch("load_game")},
                             {"relx": 0.5, "rely": 0.5, "anchor": "n", "relwidth": 0.4, "relheight": 0.2}),

            SimplifiedButton(tk, {"text": "Exit", "font": ("Comic Sans MS", 40), "command": exit},
                             {"relx": 0.5, "rely": 0.7, "anchor": "n", "relwidth": 0.4, "relheight": 0.2})
        ],
    ],

    "new_game": [
        [
            SimplifiedLabel(tk, {"text": "Name your company:", "font": ("Comic Sans MS", 40)},
                            {"relx": 0.5, "rely": 0.15, "anchor": "n"}),
        ],
        [
            SimplifiedButton(tk, {"text": "Start", "font": ("Comic Sans MS", 20), "command": lambda: new_game(PM.pages["new_game"].mediate(2, 0, 0))},
                             {"relx": 0.5, "rely": 0.4, "anchor": "n", "relwidth": 0.2, "relheight": 0.05}),

            SimplifiedButton(tk, {"text": "Back", "font": ("Comic Sans MS", 20), "command": lambda: PM.switch("main_menu")},
                             {"relx": 0.5, "rely": 0.47, "anchor": "n", "relwidth": 0.2, "relheight": 0.05}),
        ],
        [
            SimplifiedEntry(tk, {"font": ("Comic Sans MS", 20)},
                            {"relx": 0.5, "rely": 0.3, "anchor": "n"}),
        ],
    ],

    "load_game": [
        [
            SimplifiedButton(tk, {"text": "Back", "font": ("Comic Sans MS", 20), "command": lambda: PM.switch("main_menu")},
                             {"relx": 0.5, "rely": 0.47, "anchor": "n", "relwidth": 0.2, "relheight": 0.05}),
        ],
    ],

    "main_game_menu": [
        [
            SimplifiedLabel(tk, {"text": "Name", "font": ("Comic Sans MS", 50)},
                            {"relx": 0.1, "rely": 0.05, "anchor": "nw"}),

            SimplifiedLabel(tk, {"text": "Reputation", "font": ("Comic Sans MS", 35)},
                            {"relx": 0.1, "rely": 0.2, "anchor": "nw"}),

            SimplifiedLabel(tk, {"text": "Finances", "font": ("Comic Sans MS", 35)},
                            {"relx": 0.1, "rely": 0.28, "anchor": "nw"}),

            SimplifiedLabel(tk, {"text": "Shares", "font": ("Comic Sans MS", 20)},
                            {"relx": 0.1, "rely": 0.5, "anchor": "nw"}),

            SimplifiedLabel(tk, {"text": "Share price", "font": ("Comic Sans MS", 20)},
                            {"relx": 0.1, "rely": 0.55, "anchor": "nw"}),

            SimplifiedLabel(tk, {"text": "How much?", "font": ("Comic Sans MS", 20)},
                            {"relx": 0.1, "rely": 0.6, "anchor": "nw"}),

            SimplifiedLabel(tk, {"text": "Turn X", "font": ("Comic Sans MS", 20)},
                            {"relx": 0.63, "rely": 0.95, "anchor": "se"}),

            SimplifiedLabel(tk, {"text": "+$$$", "font": ("Comic Sans MS", 20)},
                            {"relx": 0.15, "rely": 0.37, "anchor": "nw"}),

            SimplifiedLabel(tk, {"text": "Currently unavailable", "font": ("Comic Sans MS", 10)},
                            {"relx": 0.05, "rely": 0.95, "anchor": "nw"}),
        ],
        [
            SimplifiedButton(tk, {"text": "Sell Shares", "font": ("Comic Sans MS", 20), "command": open_sell_shares},
                             {"relx": 0.1, "rely": 0.6, "anchor": "nw", "relwidth": 0.2, "relheight": 0.05}),

            SimplifiedButton(tk, {"text": "Sell", "font": ("Comic Sans MS", 20), "command": sell_shares},
                             {"relx": 0.1, "rely": 0.65, "anchor": "nw", "relwidth": 0.2, "relheight": 0.05}),

            SimplifiedButton(tk, {"text": "Buy Shares", "font": ("Comic Sans MS", 20), "command": open_buy_shares},
                             {"relx": 0.32, "rely": 0.6, "anchor": "nw", "relwidth": 0.2, "relheight": 0.05}),

            SimplifiedButton(tk, {"text": "Buy", "font": ("Comic Sans MS", 20), "command": buy_shares},
                             {"relx": 0.1, "rely": 0.65, "anchor": "nw", "relwidth": 0.2, "relheight": 0.05}),

            SimplifiedButton(tk, {"text": "Next turn", "font": ("Comic Sans MS", 30), "command": next_turn},
                             {"relx": 0.95, "rely": 0.95, "anchor": "se", "relwidth": 0.3, "relheight": 0.15}),

            SimplifiedButton(tk, {"text": "Cancel", "font": ("Comic Sans MS", 20), "command": cancel_buying},
                             {"relx": 0.32, "rely": 0.65, "anchor": "nw", "relwidth": 0.2, "relheight": 0.05}),

            SimplifiedButton(tk, {"text": "Cities", "font": ("Comic Sans MS", 25), "command": on_cities_page},
                             {"relx": 0.95, "rely": 0.05, "anchor": "ne", "relwidth": 0.25, "relheight": 0.15}),

            SimplifiedButton(tk, {"text": "Property", "font": ("Comic Sans MS", 25), "command": on_property_page},
                             {"relx": 0.95, "rely": 0.25, "anchor": "ne", "relwidth": 0.25, "relheight": 0.15}),

            SimplifiedButton(tk, {"text": "Save and quit", "font": ("Comic Sans MS", 25), "command": lambda: print("not awailable")},
                             {"relx": 0.05, "rely": 0.95, "anchor": "sw", "relwidth": 0.25, "relheight": 0.15}),

            SimplifiedButton(tk, {"text": "Clients", "font": ("Comic Sans MS", 25), "command": on_client_page},
                             {"relx": 0.95, "rely": 0.45, "anchor": "ne", "relwidth": 0.25, "relheight": 0.15}),
        ],
        [
            SimplifiedEntry(tk, {"font": ("Comic Sans MS", 20)},
                            {"relx": 0.25, "rely": 0.6, "anchor": "nw", "relwidth": 0.2, "relheight": 0.05}),
        ],
    ],

    "cities": [
        [
            SimplifiedLabel(tk, {"text": "Cities", "font": ("Comic Sans MS", 40)},
                            {"relx": 0.05, "rely": 0.05, "anchor": "nw"}),

            SimplifiedLabel(tk, {"text": "Taxation: 0.0", "font": ("Comic Sans MS", 30)},
                            {"relx": 0.05, "rely": 0.19, "anchor": "nw"}),

            SimplifiedLabel(tk, {"text": "Lands:", "font": ("Comic Sans MS", 30)},
                            {"relx": 0.5, "rely": 0.19, "anchor": "n"}),

            SimplifiedLabel(tk, {"text": "Finances", "font": ("Comic Sans MS", 30)},
                            {"relx": 0.95, "rely": 0.05, "anchor": "ne"}),
        ],
        [
            SimplifiedButton(tk, {"text": "Back", "font": ("Comic Sans MS", 25), "command": on_main_page},
                             {"relx": 0.95, "rely": 0.95, "anchor": "se", "relwidth": 0.25, "relheight": 0.15}),

            SimplifiedButton(tk, {"text": "Expand: 100$", "font": ("Comic Sans MS", 25), "command": expand},
                             {"relx": 0.65, "rely": 0.95, "anchor": "se", "relwidth": 0.25, "relheight": 0.15}),
        ],
        [
            SimplifiedDropList(tk, display_city, "Choose a city", {"relx": 0.15, "rely": 0.15, "relwidth": 0.2}),
        ],
        [
            CityLandInfo(tk, {"font": ("Comic Sans MS", 12), "state": "disabled"}, {"relx": 0.05, "rely": 0.3, "relwidth": 0.2, "relheight": 0.4},
                         {"text": "Buy", "font": ("Comic Sans MS", 20)}, {"relx": 0.05, "rely": 0.71}),

            CityLandInfo(tk, {"font": ("Comic Sans MS", 12), "state": "disabled"}, {"relx": 0.28, "rely": 0.3, "relwidth": 0.2, "relheight": 0.4},
                         {"text": "Buy", "font": ("Comic Sans MS", 20)}, {"relx": 0.28, "rely": 0.71}),

            CityLandInfo(tk, {"font": ("Comic Sans MS", 12), "state": "disabled"}, {"relx": 0.52, "rely": 0.3, "relwidth": 0.2, "relheight": 0.4},
                         {"text": "Buy", "font": ("Comic Sans MS", 20)}, {"relx": 0.52, "rely": 0.71}),

            CityLandInfo(tk, {"font": ("Comic Sans MS", 12), "state": "disabled"}, {"relx": 0.75, "rely": 0.3, "relwidth": 0.2, "relheight": 0.4},
                         {"text": "Buy", "font": ("Comic Sans MS", 20)}, {"relx": 0.75, "rely": 0.71}),
        ],
    ],

    "property": [
        [
            SimplifiedLabel(tk, {"text": "Lands", "font": ("Comic Sans MS", 40)},
                            {"relx": 0.05, "rely": 0.05, "anchor": "nw"}),

            SimplifiedLabel(tk, {"text": "Finances", "font": ("Comic Sans MS", 30)},
                            {"relx": 0.95, "rely": 0.05, "anchor": "ne"}),
        ],
        [
            SimplifiedButton(tk, {"text": "Back", "font": ("Comic Sans MS", 25), "command": on_main_page},
                             {"relx": 0.95, "rely": 0.95, "anchor": "se", "relwidth": 0.25, "relheight": 0.15}),

            SimplifiedButton(tk, {"text": "Auto Extend", "font": ("Comic Sans MS", 15)},
                             {"relx": 0.1, "rely": 0.65, "relwidth": 0.15, "relheight": 0.07}),

            SimplifiedButton(tk, {"text": "Raze", "font": ("Comic Sans MS", 15)},
                             {"relx": 0.45, "rely": 0.6, "anchor": "sw", "relwidth": 0.15, "relheight": 0.07}),

            SimplifiedButton(tk, {"text": "Rent out", "font": ("Comic Sans MS", 15)},
                             {"relx": 0.45, "rely": 0.3, "relwidth": 0.15, "relheight": 0.07}),

            SimplifiedButton(tk, {"text": "Build", "font": ("Comic Sans MS", 15)},
                             {"relx": 0.45, "rely": 0.6, "anchor": "sw", "relwidth": 0.15, "relheight": 0.07}),
        ],
        [
            SimplifiedDropList(tk, display_land, "Choose a land", {"relx": 0.15, "rely": 0.15, "relwidth": 0.3}),
            SimplifiedDropList(tk, display_client, "Choose a client", {"relx": 0.45, "rely": 0.2, "relwidth": 0.3}),
            SimplifiedDropList(tk, display_building, "Choose a building",
                               {"relx": 0.45, "rely": 0.5, "anchor": "sw", "relwidth": 0.3}),
        ],
        [
            PropertyInfo(tk, {"font": ("Comic Sans MS", 14), "state": "disabled"},
                         {"relx": 0.1, "rely": 0.2, "relwidth": 0.3, "relheight": 0.4}),
        ],
    ],

    "clients": [
        [
            SimplifiedLabel(tk, {"text": "Clients", "font": ("Comic Sans MS", 40)},
                            {"relx": 0.05, "rely": 0.05, "anchor": "nw"}),
        ],
        [
            SimplifiedButton(tk, {"text": "Back", "font": ("Comic Sans MS", 25), "command": on_main_page},
                             {"relx": 0.95, "rely": 0.95, "anchor": "se", "relwidth": 0.25, "relheight": 0.15}),
        ],
        [
            PropertyInfo(tk, {"font": ("Comic Sans MS", 14), "state": "disabled"},
                         {"relx": 0.5, "rely": 0.15, "anchor": "n", "relwidth": 0.35, "relheight": 0.55}),
        ],
    ],
}

commands = {
    "main_menu": [
        [SimplifiedLabel.update_text],
        [],
    ],

    "new_game": [
        [SimplifiedLabel.update_text],
        [],
        [SimplifiedEntry.get],
    ],

    "load_game": [
        [],
    ],

    "main_game_menu": [
        [SimplifiedLabel.update_text],
        [],
        [SimplifiedEntry.get, SimplifiedEntry.set],
    ],

    "cities": [
        [SimplifiedLabel.update_text],
        [],
        [SimplifiedDropList.update, SimplifiedDropList.get_list],
        [CityLandInfo.update, CityLandInfo.clear, CityLandInfo.enable, CityLandInfo.disable, CityLandInfo.set_command],
    ],

    "property": [
        [SimplifiedLabel.update_text],
        [SimplifiedButton.configure],
        [SimplifiedDropList.update, SimplifiedDropList.get_list, SimplifiedDropList.get_current, SimplifiedDropList.set],
        [PropertyInfo.update, PropertyInfo.clear]
    ],

    "clients": [
        [],
        [],
        [PropertyInfo.update, PropertyInfo.clear],
    ],
}

# load pages

PM.load("main_menu", pages, commands)

# start application

tk.protocol("WM_DELETE_WINDOW", on_closing)
tk.mainloop()
