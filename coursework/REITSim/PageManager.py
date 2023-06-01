from tkinter import *
from Mediator import *


class PageManager:

    def __init__(self, root: Tk, pages: list[str]):
        self._root = root

        self.pages = {}
        for page in pages:
            self.pages[page] = Mediator()

        self._current = ""

    def load(self, entry_page: str, style: dict[str, list[list[list[SimplifiedButton] or list[SimplifiedLabel]]]],
             commands: dict[str, list[list[classmethod]]]):
        for page in self.pages.keys():
            self.pages[page].load(style[page], commands[page])

        self.switch(entry_page)

    def switch(self, page):
        if self._current != "":
            self.pages[self._current].disable()

        if page != "":
            self.pages[page].enable()

        self._current = page


# class PageManager:
#
#     def __init__(self, root, pages):
#         self._root = root
#
#         self.pages = pages
#         self._elements = {}
#         self._entries = {}
#         for page in pages:
#             self._elements[page] = {
#                 "buttons": [],
#                 "labels": [],
#                 "entries": [],
#             }
#
#             self._entries[page] = []
#
#         self._current_page = pages[0]
#
#     def load_pages(self, settings):
#         for page in self.pages:
#             try:
#                 for options in settings[page]["buttons"]:
#                     button = MButton(self._root, *options)
#                     self._elements[page]["buttons"].append(button)
#             except KeyError:
#                 pass
#
#             try:
#                 for options in settings[page]["labels"]:
#                     label = MLabel(self._root, *options)
#                     self._elements[page]["labels"].append(label)
#             except KeyError:
#                 pass
#
#             try:
#                 for options in settings[page]["entries"]:
#                     entry = MEntry(self._root, *options)
#                     self._elements[page]["entries"].append(entry)
#                     self._entries[page].append(entry)
#             except KeyError:
#                 pass
#
#         self.switch(self.pages[0])
#
#     def switch(self, page):
#         for elements in self._elements[self._current_page]:
#             for element in self._elements[self._current_page][elements]:
#                 element.forget()
#
#         for elements in self._elements[page]:
#             for element in self._elements[page][elements]:
#                 element.place()
#
#         self._current_page = page
#
#     def get_data(self, entry_index: int):
#         return self._entries[self._current_page][entry_index].get()
