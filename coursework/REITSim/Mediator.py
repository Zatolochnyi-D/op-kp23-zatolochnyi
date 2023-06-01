from tkinter import *
from GUIComponents import *


class Mediator:

    def __init__(self):
        self._elements = []
        self._commands = []

    def load(self, components: list[list[SimplifiedButton] or list[SimplifiedLabel]],
             commands: list[list[classmethod]]) -> None:
        self._elements = components
        self._commands = commands

    def enable(self) -> None:
        for elements in self._elements:
            for element in elements:
                element.place()

    def disable(self) -> None:
        for elements in self._elements:
            for element in elements:
                element.forget()

    def on(self, target_class: int, target_object: int) -> None:
        self._elements[target_class][target_object].place()

    def off(self, target_class: int, target_object: int) -> None:
        self._elements[target_class][target_object].forget()

    def mediate(self, target_class: int, target_object: int, command: int, *args, **kwargs) -> any:
        return self._commands[target_class][command](self._elements[target_class][target_object], *args, **kwargs)
