from tkinter import *
from PageManager import *
from GameManager import GameManager

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

PM = PageManager(tk, ["main_menu", "new_game", "load_game", "main_game_menu"])
GAME: GameManager or None = None

# define functions, add pages content and available commands


def new_game(name):
    global GAME
    GAME = GameManager(name)

    PM.switch("main_game_menu")

    info = game.get_player_stats()
    PM.pages["main_game_menu"].mediate(0, 0, 0, info[0])
    PM.pages["main_game_menu"].mediate(0, 1, 0, info[1])
    PM.pages["main_game_menu"].mediate(0, 2, 0, info[2])


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
            SimplifiedLabel(tk, {"text": "", "font": ("Comic Sans MS", 40)},
                            {"relx": 0.5, "rely": 0.15, "anchor": "n"}),

            SimplifiedLabel(tk, {"text": "", "font": ("Comic Sans MS", 40)},
                            {"relx": 0.5, "rely": 0.35, "anchor": "n"}),

            SimplifiedLabel(tk, {"text": "", "font": ("Comic Sans MS", 40)},
                            {"relx": 0.5, "rely": 0.55, "anchor": "n"}),
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
    ],
}

# load pages

PM.load("main_menu", pages, commands)

# start application

tk.mainloop()
