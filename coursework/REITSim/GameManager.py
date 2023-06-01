from os import getcwd
from pythonnet import load

load("coreclr")
import clr

clr.AddReference(getcwd() + "/bin/Debug/net7.0/REITSim.dll")

import GameMechanics as gm


class GameManager:

    def __init__(self, name):
        self._game = gm.World(name)

    def get_player_stats(self) -> list:
        return [self._game.Player.Name, self._game.Player.Reputation, self._game.Player.Money,
                self._game.Player.Shares, self._game.Player.SharePrice, self._game.Player.SharesOnExchange]

    def sell_shares(self, input: str) -> None:
        try:
            amount = float(input)
        except ValueError:
            pass
        else:
            self._game.Player.SharesToSell(amount)

    def buy_shares(self, input: str) -> None:
        try:
            amount = float(input)
        except ValueError:
            pass
        else:
            self._game.Player.BuyShares(amount)

    def next_turn(self) -> None:
        self._game.Player.NextTurn()
