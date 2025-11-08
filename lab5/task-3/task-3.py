import tkinter as tk
from tkinter import ttk, messagebox

class Taburetka:
    def __init__(self, h, yakist, kolir):
        self.h = h
        self.yakist = yakist.lower()
        self.kolir = kolir

    def kilkist_derevyny(self):
        if self.yakist == "низька":
            return 4 * self.h + 12
        elif self.yakist in ("середня", "висока"):
            return 5 * self.h + 14
        else:
            raise ValueError("Невірно вказано якість.")

    def vartist(self):
        d = self.kilkist_derevyny()
        if self.yakist == "низька": 
            return d * 2
        elif self.yakist == "середня":
            return d * 3
        elif self.yakist == "висока":
            return d * 4

    def informatsiya(self):
        d = self.kilkist_derevyny()
        v = self.vartist()
        return f"Табуретка:\nВисота: {self.h} см\nЯкість: {self.yakist}\nКолір: {self.kolir}\nДеревина: {d}\nВартість: {v} грн"

class Stilets(Taburetka):
    def __init__(self, h, yakist, kolir):
        super().__init__(h, yakist, kolir)
        self.vysota_spynky = 2 * h

    def kilkist_derevyny(self):
        d = super().kilkist_derevyny()
        return 5 * d / 2 + 2 * self.h

    def vartist(self):
        d = self.kilkist_derevyny()
        if self.yakist == "висока":
            return d * 4.5
        elif self.yakist == "середня":
            return d * 3.5
        else:
            return d * 2.5

    def informatsiya(self):
        d = self.kilkist_derevyny()
        v = self.vartist()
        return (f"Стілець:\nВисота: {self.h} см\nСпинка: {self.vysota_spynky} см\n"
                f"Якість: {self.yakist}\nКолір: {self.kolir}\nДеревина: {round(d, 2)}\nВартість: {round(v, 2)} грн")

class Stil(Taburetka):
    def __init__(self, h, yakist, kolir):
        super().__init__(h, yakist, kolir)
        self.shyryna = 3 * h

    def kilkist_derevyny(self):
        d = super().kilkist_derevyny()
        return d + 2 * self.h

    def vartist(self):
        d = self.kilkist_derevyny()
        if self.yakist == "висока":
            return d * 10
        elif self.yakist == "середня":
            return d * 7
        else:
            return d * 5

    def informatsiya(self):
        d = self.kilkist_derevyny()
        v = self.vartist()
        return (f"Стіл:\nВисота: {self.h} см\nШирина: {self.shyryna} см\n"
                f"Якість: {self.yakist}\nКолір: {self.kolir}\nДеревина: {round(d, 2)}\nВартість: {round(v, 2)} грн")

class Application(tk.Tk):
    def __init__(self):
        super().__init__()
        self.title("Меблі: Табуретка, Стілець, Стіл")
        self.geometry("600x600")
        self.configure(bg="#f0f0f0")

        # Поля введення
        ttk.Label(self, text="Висота (см):").pack()
        self.entry_h = ttk.Entry(self)
        self.entry_h.pack()

        ttk.Label(self, text="Якість (низька / середня / висока):").pack()
        self.entry_yakist = ttk.Entry(self)
        self.entry_yakist.pack()

        ttk.Label(self, text="Колір деревини:").pack()
        self.entry_kolir = ttk.Entry(self)
        self.entry_kolir.pack()

        ttk.Button(self, text="Створити Табуретку", command=self.create_taburetka).pack(pady=5)
        ttk.Button(self, text="Створити Стілець", command=self.create_stilets).pack(pady=5)
        ttk.Button(self, text="Створити Стіл", command=self.create_stil).pack(pady=5)

        ttk.Label(self, text="Результат:").pack()
        self.output = tk.Text(self, height=15, width=70)
        self.output.pack()

        ttk.Button(self, text="Очистити", command=self.clear).pack(pady=10)

    def clear(self):
        self.output.delete(1.0, tk.END)

    def create_taburetka(self):
        try:
            h = int(self.entry_h.get())
            yakist = self.entry_yakist.get()
            kolir = self.entry_kolir.get()
            obj = Taburetka(h, yakist, kolir)
            self.output.insert(tk.END, obj.informatsiya() + "\n\n")
        except Exception as e:
            messagebox.showerror("Помилка", str(e))

    def create_stilets(self):
        try:
            h = int(self.entry_h.get())
            yakist = self.entry_yakist.get()
            kolir = self.entry_kolir.get()
            obj = Stilets(h, yakist, kolir)
            self.output.insert(tk.END, obj.informatsiya() + "\n\n")
        except Exception as e:
            messagebox.showerror("Помилка", str(e))

    def create_stil(self):
        try:
            h = int(self.entry_h.get())
            yakist = self.entry_yakist.get()
            kolir = self.entry_kolir.get()
            obj = Stil(h, yakist, kolir)
            self.output.insert(tk.END, obj.informatsiya() + "\n\n")
        except Exception as e:
            messagebox.showerror("Помилка", str(e))

if __name__ == "__main__":
    app = Application()
    app.mainloop()
