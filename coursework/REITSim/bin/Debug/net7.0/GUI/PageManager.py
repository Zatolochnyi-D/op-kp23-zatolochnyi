from tkinter import *
from GUI.Mediator import *


class PageManager:

    def __init__(self, root: Tk, pages: list[str]):
        self._root = root

        self.pages = {}
        for page in pages:
            self.pages[page] = Mediator()

        self._current = ""

    def load(self, entry_page: str, style, commands):
        for page in self.pages.keys():
            self.pages[page].load(style[page], commands[page])

        self.switch(entry_page)

    def switch(self, page):
        if self._current != "":
            self.pages[self._current].disable()

        if page != "":
            self.pages[page].enable()

        self._current = page
