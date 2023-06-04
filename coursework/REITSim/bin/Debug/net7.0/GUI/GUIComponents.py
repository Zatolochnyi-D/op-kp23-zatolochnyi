from tkinter import *


class SimplifiedLabel:

    def __init__(self, root: Tk or Frame, options: dict[str, any], placement: dict[str, any]):
        self._label = Label(root, **options)
        self._placement = placement

    def place(self) -> None:
        self._label.place(**self._placement)

    def forget(self) -> None:
        self._label.place_forget()

    def update_text(self, text: str) -> None:
        self._label["text"] = text


class SimplifiedButton:

    def __init__(self, root: Tk or Frame, options: dict[str, any], placement: dict[str, any]):
        self._button = Button(root, **options)
        self._placement = placement

    def place(self) -> None:
        self._button.place(**self._placement)

    def forget(self) -> None:
        self._button.place_forget()


class SimplifiedEntry:

    def __init__(self, root: Tk or Frame, options: dict[str, any], placement: dict[str, any]):
        self._variable = StringVar()
        self._entry = Entry(root, textvariable=self._variable, **options)
        self._placement = placement

    def place(self) -> None:
        self._entry.place(**self._placement)

    def forget(self) -> None:
        self._entry.place_forget()

    def get(self) -> str:
        return self._variable.get()

    def set(self, text: str) -> None:
        self._variable.set(text)


class SimplifiedImage:

    def __init__(self, root: Tk or Frame, image: str, options: dict[str, any], placement: dict[str, any]):
        self._canvas = Canvas(root, **options)
        self._image = PhotoImage(file=image)
        self._canvas.create_image(0, 0, image=self._image)
        self._placement = placement

    def place(self):
        self._canvas.place(**self._placement)

    def forget(self):
        self._canvas.place_forget()


class CityItem:

    def __init__(self, root: Tk or Frame):
        self._frame = Frame(root)
        # TODO: create layout with city info
