import re
import tkinter as tk
from tkinter import messagebox


def find_math_symbols():
    text = text_input.get("1.0", tk.END)
    pattern = r"[+\-*/=<>%^]"
    matches = re.findall(pattern, text)

    result_list.delete(0, tk.END)
    for m in matches:
        result_list.insert(tk.END, m)

    count_label.config(text=f"Кількість знайдених символів: {len(matches)}")

def remove_symbol():
    symbol = symbol_entry.get()
    if not symbol:
        messagebox.showwarning("Увага", "Введіть символ для вилучення!")
        return

    text = text_input.get("1.0", tk.END)
    new_text = text.replace(symbol, "")
    text_input.delete("1.0", tk.END)
    text_input.insert("1.0", new_text)


def replace_symbol():
    old = symbol_entry.get()
    new = new_entry.get()
    if not old or not new:
        messagebox.showwarning("Увага", "Введіть символ для заміни та новий символ!")
        return

    text = text_input.get("1.0", tk.END)
    new_text = text.replace(old, new)
    text_input.delete("1.0", tk.END)
    text_input.insert("1.0", new_text)


root = tk.Tk()
root.title("Math Symbols Finder")
root.geometry("600x500")

tk.Label(root, text="Введіть текст:").pack(anchor="w", padx=10, pady=5)
text_input = tk.Text(root, height=8, width=70)
text_input.pack(padx=10, pady=5)

tk.Button(root, text="Знайти символи", command=find_math_symbols).pack(pady=5)

tk.Label(root, text="Знайдені символи:").pack(anchor="w", padx=10)
result_list = tk.Listbox(root, width=30, height=6)
result_list.pack(padx=10, pady=5)

count_label = tk.Label(root, text="Кількість знайдених символів: 0")
count_label.pack()

frame = tk.Frame(root)
frame.pack(pady=10)

tk.Label(frame, text="Символ:").grid(row=0, column=0, padx=5)
symbol_entry = tk.Entry(frame, width=5)
symbol_entry.grid(row=0, column=1, padx=5)

tk.Button(frame, text="Вилучити", command=remove_symbol).grid(row=0, column=2, padx=10)

tk.Label(frame, text="Новий:").grid(row=0, column=3, padx=5)
new_entry = tk.Entry(frame, width=5)
new_entry.grid(row=0, column=4, padx=5)

tk.Button(frame, text="Замінити", command=replace_symbol).grid(row=0, column=5, padx=10)

root.mainloop()
