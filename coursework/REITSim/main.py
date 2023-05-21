from tkinter import *
import os
import clr

pathToDll = os.getcwd() + "/bin/Debug/net7.0/REITSim.dll"
clr.AddReference(pathToDll)

import GameMechanics as gm

tk = Tk()
player = gm.Player()

btn = Button(text="PlayerMoney", command=lambda: print(player.Money))

btn.pack()

tk.mainloop()
