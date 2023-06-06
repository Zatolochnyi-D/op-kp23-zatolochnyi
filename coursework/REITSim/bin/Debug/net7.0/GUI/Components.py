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

    def configure(self, options):
        self._button.configure(**options)


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


class SimplifiedDropList:
    def __init__(self, root: Tk, command, reset_text, placement):
        self._variable = StringVar(root, value=reset_text)
        self._root = root
        self._options = ["None"]
        self._command = command
        self._reset_text = reset_text

        self._drop_list = OptionMenu(self._root, self._variable, *self._options, command=self._command)

        self._placement = placement

    def place(self):
        self._drop_list.place(**self._placement)

    def forget(self):
        self._drop_list.place_forget()
        self._variable.set(self._reset_text)

    def update(self, options):
        self._options = options
        self.forget()
        self._drop_list = OptionMenu(self._root, self._variable, *self._options, command=self._command)
        self.place()

    def get_list(self):
        return self._options

    def get_current(self):
        return self._variable.get()


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


class PropertyInfo:

    def __init__(self, root: Tk, options, placement, button_options, ibutton_placement, cbutton_placement,
                 clients_placement, command):
        self._textbox = Text(root, **options)
        self._placement = placement

        self._interact_button = SimplifiedButton(root, button_options, ibutton_placement)

        self._client_button = SimplifiedButton(root, button_options, cbutton_placement)
        self._clients = SimplifiedDropList(root, command, "Choose a client", clients_placement)

    def place(self):
        self._textbox.place(self._placement)
        self._interact_button.place()
        self._client_button.place()
        self._clients.place()

    def forget(self):
        self._textbox.place_forget()
        self._interact_button.forget()
        self._client_button.forget()
        self._clients.forget()

    def update_text(self, info):
        self._textbox.config(state="normal")
        self._textbox.delete('1.0', END)

        for line in info:
            self._textbox.insert(END, line)

        self._textbox.config(state="disabled")

    def clear(self):
        self._textbox.config(state="normal")

        self._textbox.delete('1.0', END)

        self._textbox.config(state="disabled")

    def enable_interact(self):
        self._interact_button.place()

    def disable_interact(self):
        self._interact_button.forget()

    def enable_client(self):
        self._clients.place()
        self._client_button.place()

    def disable_client(self):
        self._clients.forget()
        self._client_button.forget()

    def interact_button(self, options):
        self._interact_button.configure(options)

    def client_button(self, options):
        self._client_button.configure(options)

    def update_client_options(self, options):
        self._clients.update(options)

    def get_clients_list(self):
        return self._clients.get_list()

    def get_cur_client(self):
        return self._clients.get_current()
