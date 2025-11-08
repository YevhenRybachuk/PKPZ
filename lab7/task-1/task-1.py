import tkinter as tk
from tkinter import messagebox
from datetime import datetime

def calculate_time():
    try:
        dep_time_str = entry_departure.get()
        ret_time_str = entry_return.get()

        dep_time = datetime.strptime(dep_time_str, "%H:%M:%S")
        ret_time = datetime.strptime(ret_time_str, "%H:%M:%S")

        if ret_time < dep_time:
            ret_time = ret_time.replace(day=2)

        delta = ret_time - dep_time
        total_minutes = int(delta.total_seconds() // 60)
        total_hours = total_minutes // 60

        label_result.config(
            text=f"Час на маршруті: {total_minutes} хвилин ({total_hours} годин)"
        )

    except ValueError:
        messagebox.showerror("Помилка", "Невірний формат! Введіть час як ГГ:ХХ:СС")

window = tk.Tk()
window.title("Розрахунок часу роботи трамвая")
window.geometry("400x250")
window.resizable(False, False)

tk.Label(window, text="Час виходу (ГГ:ХХ:СС):", font=("Arial", 12)).pack(pady=5)
entry_departure = tk.Entry(window, font=("Arial", 12), justify="center")
entry_departure.pack(pady=5)

tk.Label(window, text="Час повернення (ГГ:ХХ:СС):", font=("Arial", 12)).pack(pady=5)
entry_return = tk.Entry(window, font=("Arial", 12), justify="center")
entry_return.pack(pady=5)

tk.Button(window, text="Обчислити", font=("Arial", 12), command=calculate_time).pack(pady=10)

label_result = tk.Label(window, text="", font=("Arial", 12, "bold"), fg="blue")
label_result.pack(pady=10)

window.mainloop()
