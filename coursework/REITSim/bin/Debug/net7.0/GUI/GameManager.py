from os import getcwd
from pythonnet import load

load("coreclr")
import clr

clr.AddReference(getcwd() + "/REITSim.dll")

import GameMechanics as gm


class GameManager:

    def __init__(self, name):
        self._game = gm.World(name)

    @property
    def turn(self) -> int:
        return self._game.Turn

    def get_player_stats(self) -> list:
        return [self._game.Player.Name, self._game.Player.Reputation, self._game.Player.Money,
                self._game.Player.Shares, self._game.Player.SharePrice, self._game.Player.SharesOnExchange,
                self._game.Player.Income]

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

    def get_cities(self):
        return self._game.Cities

    def buy_land(self, land):
        self._game.Player.BuyLand(land)

    def update_income(self):
        self._game.Player.UpdateIncome()

    def expand(self):
        self._game.Expand()

    def get_property(self):
        return self._game.Player.Property

    def get_clients(self):
        return self._game.Clients

    def raze_building(self, land):
        self._game.Player.RazeBuilding(land)

    def rent_out(self, land, client):
        self._game.Player.RentOutBuilding(land, client)

    def next_turn(self) -> None:
        self._game.NextTurn()
