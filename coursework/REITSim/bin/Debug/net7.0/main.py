from tkinter import *
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

PM = PageManager(tk, ["main_menu", "new_game", "load_game", "main_game_menu", "cities", "property"])
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

    names = []
    for city in GAME.get_cities():
        names.append(city.Name)

    mediator.mediate(2, 0, 0, names)

    for i in range(4):
        mediator.mediate(3, i, 1)
        mediator.mediate(3, i, 3)


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
            info.append("You already have this land")
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


def next_turn() -> None:
    GAME.next_turn()
    update_main_page()


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

            SimplifiedButton(tk, {"text": "Property", "font": ("Comic Sans MS", 25), "command": lambda: PM.switch("property")},
                             {"relx": 0.95, "rely": 0.25, "anchor": "ne", "relwidth": 0.25, "relheight": 0.15}),

            SimplifiedButton(tk, {"text": "Save and quit", "font": ("Comic Sans MS", 25), "command": lambda: print("not awailable")},
                             {"relx": 0.05, "rely": 0.95, "anchor": "sw", "relwidth": 0.25, "relheight": 0.15}),
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
        ],
        [
            SimplifiedButton(tk, {"text": "Back", "font": ("Comic Sans MS", 25), "command": on_main_page},
                             {"relx": 0.95, "rely": 0.95, "anchor": "se", "relwidth": 0.25, "relheight": 0.15}),
        ],
        [
            SimplifiedDropList(tk, display_city, {"relx": 0.15, "rely": 0.15, "relwidth": 0.2}),
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
            SimplifiedButton(tk, {"text": "Back", "font": ("Comic Sans MS", 25), "command": on_main_page},
                             {"relx": 0.95, "rely": 0.95, "anchor": "se", "relwidth": 0.25, "relheight": 0.15}),
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
        [],
    ],
}

# load pages

PM.load("main_menu", pages, commands)

# start application

tk.mainloop()
