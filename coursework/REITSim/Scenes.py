from tkinter import *

global_settings = {
    "width": 800,
    "height": 600,
    "width_resize": False,
    "height_resize": False,
}

pages = {
    "main menu": {
        
        "buttons": [
            [{"text": "New Game", "font": ("Comic Sans MS", 40)},
             {"relx": 0.5, "rely": 0.3, "anchor": "n", "relwidth": 0.4, "relheight": 0.2}],

            [{"text": "Load Game", "font": ("Comic Sans MS", 40)},
             {"relx": 0.5, "rely": 0.5, "anchor": "n", "relwidth": 0.4, "relheight": 0.2}],

            [{"text": "Exit", "font": ("Comic Sans MS", 40)},
             {"relx": 0.5, "rely": 0.7, "anchor": "n", "relwidth": 0.4, "relheight": 0.2}],
        ],
        
        "labels": [
            [{"text": "REIT Simulator", "font": ("Comic Sans MS", 60)},
             {"relx": 0.5, "rely": 0.05, "anchor": "n"}],
        ],
    },
}
