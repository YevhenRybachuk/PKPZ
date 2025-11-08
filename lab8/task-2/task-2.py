import tkinter as tk
from tkinter import messagebox
from collections import namedtuple

Team = namedtuple('Team', ['name', 'score', 'coach', 'city'])

teams = (
    Team('Леви', 56, 'Іванчук', 'Львів'),
    Team('Соколи', 48, 'Карп', 'Київ'),
    Team('Буревісник', 62, 'Шевченко', 'Одеса'),
    Team('Тигри', 40, 'Ковальчук', 'Харків'),
    Team('Дракони', 70, 'Мельник', 'Дніпро'),
    Team('Шторм', 53, 'Корецький', 'Запоріжжя'),
    Team('Скорпіони', 45, 'Ткаченко', 'Черкаси')
)

def tourney(teams):
    avg_score = sum(team.score for team in teams) / len(teams)
    losers = [team.name for team in teams if team.score < avg_score]
    return avg_score, losers

class App(tk.Tk):
    def __init__(self):
        super().__init__()
        self.title("Чемпіонат команд")
        self.geometry("500x400")
        self.teams = teams

        tk.Label(self, text="Список команд:", font=("Arial", 12, "bold")).pack(pady=5)

        self.listbox = tk.Listbox(self, width=50, height=10)
        self.listbox.pack()
        self.update_listbox()

        tk.Button(self, text="Показати результат турніру", command=self.show_results).pack(pady=10)
        tk.Button(self, text="Додати +10 балів усім командам", command=self.add_points).pack(pady=5)

        self.label_info = tk.Label(self, text="", fg="blue", font=("Arial", 11))
        self.label_info.pack(pady=10)

    def update_listbox(self):
        self.listbox.delete(0, tk.END)
        for t in self.teams:
            self.listbox.insert(tk.END, f"{t.name} ({t.city}) — {t.score} балів, тренер: {t.coach}")

    def show_results(self):
        avg, losers = tourney(self.teams)
        if losers:
            msg = f"Середній бал: {avg:.2f}\nКоманди, які не пройшли відбір:\n" + ", ".join(losers)
        else:
            msg = f"Середній бал: {avg:.2f}\nУсі команди пройшли!"
        messagebox.showinfo("Результати", msg)
        self.label_info.config(text=f"Середній бал: {avg:.2f}")

    def add_points(self):
        self.teams = tuple(t._replace(score=t.score + 10) for t in self.teams)
        self.update_listbox()
        messagebox.showinfo("Оновлення", "Усім командам додано +10 балів!")
        self.show_results()

if __name__ == "__main__":
    app = App()
    app.mainloop()
