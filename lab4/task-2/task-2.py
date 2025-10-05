import tkinter as tk
from tkinter import messagebox, scrolledtext
import os

def create_tf1():
    lines = [
        "Hello123 world456!",
        "bla14 bla88",
        "Hello0 Ded2",
        "314pets",
        "Short1",
        "Long line 98765",
    ]
    with open("TF_1.txt", "w", encoding="utf-8") as f:
        for line in lines:
            f.write(line + "\n")
    messagebox.showinfo("Файл створено", "Файл TF_1.txt успішно створено!")

def process_file():
    if not os.path.exists("TF_1.txt"):
        messagebox.showerror("Помилка", "Файл TF_1.txt не знайдено!")
        return

    with open("TF_1.txt", "r", encoding="utf-8") as f:
        text = f.read().replace("\n", " ")

    cleaned_text = ''.join(ch for ch in text if not ch.isdigit())

    cleaned_text = ' '.join(cleaned_text.split())

    chunks = [cleaned_text[i:i+10] for i in range(0, len(cleaned_text), 10)]

    with open("TF_2.txt", "w", encoding="utf-8") as f:
        for chunk in chunks:
            f.write(chunk + "\n")

    messagebox.showinfo("Оброблено", "Файл TF_2.txt успішно створено!")

def show_tf2():
    if not os.path.exists("TF_2.txt"):
        messagebox.showerror("Помилка", "Файл TF_2.txt не знайдено!")
        return

    with open("TF_2.txt", "r", encoding="utf-8") as f:
        lines = f.readlines()

    output_text.delete(1.0, tk.END)
    for line in lines:
        output_text.insert(tk.END, line)

root = tk.Tk()
root.title("Лабораторна 1.2 — Робота з файлами")
root.geometry("600x400")
root.resizable(False, False)

frame = tk.Frame(root, padx=10, pady=10)
frame.pack(fill=tk.BOTH, expand=True)

tk.Label(frame, text="Оберіть дію:", font=("Arial", 14)).pack(pady=5)

tk.Button(frame, text="Створити TF_1.txt", font=("Arial", 12), width=25, command=create_tf1).pack(pady=5)
tk.Button(frame, text="Обробити файл (створити TF_2.txt)", font=("Arial", 12), width=25, command=process_file).pack(pady=5)
tk.Button(frame, text="Показати TF_2.txt", font=("Arial", 12), width=25, command=show_tf2).pack(pady=5)

tk.Label(frame, text="Вміст TF_2.txt:", font=("Arial", 12)).pack(pady=10)
output_text = scrolledtext.ScrolledText(frame, width=70, height=10, font=("Consolas", 11))
output_text.pack()

root.mainloop()
