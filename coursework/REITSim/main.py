from tkinter import *
from SceneManager import SceneManager
from Scenes import *
import os
import clr

# pathToDll = os.getcwd() + "/bin/Debug/net7.0/REITSim.dll"
# clr.AddReference(pathToDll)
#
# import GameMechanics as gm

tk = Tk()
tk.geometry(f"{global_settings['width']}x{global_settings['height']}")
tk.resizable(global_settings["width_resize"], global_settings["height_resize"])

sc = SceneManager(tk, ["main menu"])
sc.load_scenes(pages)

tk.mainloop()
