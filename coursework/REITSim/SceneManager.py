from tkinter import *


class SceneManager:

    def __init__(self, root, pages):
        self._root = root

        self._pages = pages
        self._elements = {}
        for page in pages:
            self._elements[page] = {
                "buttons": [],
                "labels": [],
            }

    def load_scenes(self, settings):
        for page in self._pages:
            for options in settings[page]["buttons"]:
                button = MButton(self._root, *options)
                button.place()
                self._elements[page]["buttons"].append(button)

            for options in settings[page]["labels"]:
                label = MLabel(self._root, *options)
                label.place()
                self._elements[page]["labels"].append(label)


class MButton:

    def __init__(self, root, button_options, place_options):
        self._button = Button(root, **button_options)
        self._place_options = place_options

    def place(self):
        self._button.place(**self._place_options)

    def forget(self):
        self._button.place_forget()


class MLabel:

    def __init__(self, root, label_options, place_options):
        self._label = Label(root, **label_options)
        self._place_options = place_options

    def place(self):
        self._label.place(**self._place_options)

    def forget(self):
        self._label.place_forget()
