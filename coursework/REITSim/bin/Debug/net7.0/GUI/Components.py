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

    def set_command(self, command):
        self._button.config(command=command)


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


class SimplifiedDropList:
    def __init__(self, root: Tk, command, placement):
        self._variable = StringVar(root, value="Choose a city")
        self._root = root
        self._options = ["None"]
        self._command = command

        self._drop_list = OptionMenu(self._root, self._variable, *self._options, command=self._command)

        self._placement = placement

    def place(self):
        self._drop_list.place(**self._placement)

    def forget(self):
        self._drop_list.place_forget()
        self._variable.set("Choose a city")

    def update(self, options):
        self._options = options
        self.forget()
        self._drop_list = OptionMenu(self._root, self._variable, *self._options, command=self._command)
        self.place()

    def get_list(self):
        return self._options


class CityLandInfo:

    def __init__(self, root: Tk, options, placement, button_options, button_placement):
        self._textbox = Text(root, **options)
        self._textbox_placement = placement

        self._button = SimplifiedButton(root, button_options, button_placement)

    def place(self):
        self._textbox.place(**self._textbox_placement)
        self._button.place()

    def forget(self):
        self._textbox.place_forget()
        self._button.forget()

    def update(self, info):
        self._textbox.config(state="normal")
        self._textbox.delete('1.0', END)

        for line in info:
            self._textbox.insert(END, line)

        self._textbox.config(state="disabled")

    def clear(self):
        self._textbox.config(state="normal")

        self._textbox.delete('1.0', END)

        self._textbox.config(state="disabled")

    def enable(self):
        self._button.place()

    def disable(self):
        self._button.forget()

    def set_command(self, command):
        self._button.set_command(command)
