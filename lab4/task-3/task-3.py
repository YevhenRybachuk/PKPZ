import tkinter as tk
from tkinter import messagebox, filedialog
import math
import os
import traceback

INPUT_FILENAME = "input.txt"
OUTPUT_FILENAME = "output.txt"
SESSION_LOG_FILENAME = "session_log.txt"

class CalculatorApp:
    def __init__(self, root):
        self.root = root
        self.root.title("Арифметичний калькулятор — Лабораторна")
        self.action_counter = 0
        self.n1 = None
        self.n2 = None
        self.operation = tk.StringVar(value="+")
        self.result_text = tk.StringVar(value="")
        self.status_text = tk.StringVar(value="Готово")
        self._clear_session_log()
        self._log_action("додаток запущено")
        self._build_gui()
        self.root.protocol("WM_DELETE_WINDOW", self._on_close)

    def _clear_session_log(self):
        try:
            with open(SESSION_LOG_FILENAME, "w", encoding="utf-8") as f:
                pass
        except Exception:
            print("Не вдалося очистити session log:", traceback.format_exc())

    def _log_action(self, text):
        self.action_counter += 1
        entry = f"Дія {self.action_counter}: {text}\n"
        try:
            with open(SESSION_LOG_FILENAME, "a", encoding="utf-8") as f:
                f.write(entry)
        except Exception:
            print("Помилка логування:", traceback.format_exc())

    def _build_gui(self):
        frm_top = tk.Frame(self.root, padx=10, pady=10)
        frm_top.pack(fill=tk.X)

        tk.Label(frm_top, text="Параметр 1:").grid(row=0, column=0, sticky="e")
        self.lbl_n1 = tk.Label(frm_top, text="—")
        self.lbl_n1.grid(row=0, column=1, sticky="w", padx=5)

        tk.Label(frm_top, text="Параметр 2:").grid(row=1, column=0, sticky="e")
        self.lbl_n2 = tk.Label(frm_top, text="—")
        self.lbl_n2.grid(row=1, column=1, sticky="w", padx=5)

        ops_frame = tk.LabelFrame(self.root, text="Оберіть арифметичну операцію", padx=10, pady=10)
        ops_frame.pack(fill=tk.X, padx=10)
        ops = [("+", "Додавання (+)"), ("-", "Віднімання (-)"), ("*", "Множення (*)"),
               ("/", "Ділення (/)"), ("^", "Піднесення в степінь (^ )")]
        for i, (val, lbl) in enumerate(ops):
            rb = tk.Radiobutton(ops_frame, text=lbl, variable=self.operation, value=val,
                                command=lambda v=val: self._on_operation_selected(v))
            rb.grid(row=0, column=i, padx=5, sticky="w")

        btn_frame = tk.Frame(self.root, pady=10)
        btn_frame.pack(fill=tk.X, padx=10)
        btn_import = tk.Button(btn_frame, text="Імпортувати вхідні дані", command=self.import_input)
        btn_import.grid(row=0, column=0, padx=5)
        btn_compute = tk.Button(btn_frame, text="Обчислити вираз", command=self.compute_expression)
        btn_compute.grid(row=0, column=1, padx=5)
        btn_export = tk.Button(btn_frame, text="Експортувати результат у файл", command=self.export_result)
        btn_export.grid(row=0, column=2, padx=5)

        res_frame = tk.Frame(self.root, padx=10, pady=10)
        res_frame.pack(fill=tk.X)
        tk.Label(res_frame, text="Результат:").grid(row=0, column=0, sticky="e")
        self.lbl_result = tk.Label(res_frame, textvariable=self.result_text, width=40, anchor="w", relief="sunken")
        self.lbl_result.grid(row=0, column=1, padx=5, sticky="w")

        status_frame = tk.Frame(self.root, padx=10, pady=5)
        status_frame.pack(fill=tk.X)
        tk.Label(status_frame, text="Статус:").grid(row=0, column=0, sticky="e")
        tk.Label(status_frame, textvariable=self.status_text, anchor="w").grid(row=0, column=1, sticky="w", padx=5)

        files_frame = tk.Frame(self.root, padx=10, pady=5)
        files_frame.pack(fill=tk.X)
        tk.Label(files_frame, text=f"Input file: {INPUT_FILENAME}").grid(row=0, column=0, sticky="w")
        tk.Label(files_frame, text=f"Output file: {OUTPUT_FILENAME}").grid(row=1, column=0, sticky="w")
        tk.Label(files_frame, text=f"Session log: {SESSION_LOG_FILENAME}").grid(row=2, column=0, sticky="w")

    def _on_operation_selected(self, val):
        self._log_action(f"обрано арифметичну операцію «{val}»")
        self.status_text.set(f"Операція: {val}")

    def import_input(self):
        self._log_action("обрано «Імпортувати вхідні дані»")
        try:
            if not os.path.exists(INPUT_FILENAME):
                messagebox.showwarning("Файл відсутній", f"Файл '{INPUT_FILENAME}' не знайдено. Створіть файл з двома числами.")
                self.status_text.set("Файл відсутній")
                return

            with open(INPUT_FILENAME, "r", encoding="utf-8") as f:
                content = f.read().strip()

            if content == "":
                messagebox.showerror("Файл порожній", "Файл порожній, введіть дані")
                self.status_text.set("Файл порожній")
                self._log_action("повідомлення: Файл порожній, введіть дані")
                return

            parts = content.split()
            if len(parts) < 2:
                messagebox.showerror("Недопустимі значення", "Файл повинен містити два числа (перший та другий параметр).")
                self.status_text.set("Недопустимі значення")
                self._log_action("повідомлення: Недопустимі значення введених параметрів (недостатньо значень)")
                return

            try:
                n1 = float(parts[0])
                n2 = float(parts[1])
            except ValueError:
                messagebox.showerror("Недопустимі значення", "Недопустимі значення введених параметрів")
                self.status_text.set("Недопустимі значення")
                self._log_action("повідомлення: Недопустимі значення введених параметрів (не числа)")
                return

            self.n1 = n1
            self.n2 = n2
            def fmt(x):
                return str(int(x)) if x.is_integer() else str(x)

            self.lbl_n1.config(text=fmt(self.n1))
            self.lbl_n2.config(text=fmt(self.n2))
            self.status_text.set("Дані імпортовано")
        except Exception as ex:
            messagebox.showerror("Помилка імпорту", f"Сталася помилка при імпорті: {ex}")
            self.status_text.set("Помилка імпорту")
            self._log_action("помилка при імпорті")
            print(traceback.format_exc())

    def compute_expression(self):
        self._log_action("обрано «Обчислити вираз»")
        try:
            if self.n1 is None or self.n2 is None:
                messagebox.showwarning("Дані не імпортовано", "Спочатку імпортуйте вхідні дані.")
                self.status_text.set("Дані не імпортовано")
                return

            op = self.operation.get()
            if op == "/" and self.n2 == 0:
                messagebox.showerror("Ділення на 0", "Ділення на 0 заборонено")
                self.status_text.set("Ділення на 0 заборонено")
                self._log_action("повідомлення: Ділення на 0 заборонено")
                return

            try:
                if op == "+":
                    res = self.n1 + self.n2
                elif op == "-":
                    res = self.n1 - self.n2
                elif op == "*":
                    res = self.n1 * self.n2
                elif op == "/":
                    res = self.n1 / self.n2
                elif op == "^":
                    res = math.pow(self.n1, self.n2)
                else:
                    raise ValueError("Невідома операція")
            except Exception as e_calc:
                messagebox.showerror("Помилка обчислення", f"Під час обчислення сталася помилка: {e_calc}")
                self.status_text.set("Помилка обчислення")
                self._log_action("помилка обчислення")
                return

            def fmt(x):
                return str(int(x)) if isinstance(x, float) and x.is_integer() else str(x)

            formatted = f"{fmt(self.n1)} {op} {fmt(self.n2)}, Результат: {fmt(res)}"
            self.result_text.set(formatted)
            self.status_text.set("Обчислено")
            self._log_action(f"обчислено вираз: {formatted}")
        finally:
            pass

    def export_result(self):
        self._log_action("обрано «Експортувати результат у файл»")
        try:
            if not self.result_text.get():
                messagebox.showwarning("Немає результату", "Спочатку обчисліть вираз, потім експортуйте результат.")
                self.status_text.set("Немає результату для експорту")
                return

            try:
                with open(OUTPUT_FILENAME, "w", encoding="utf-8") as f:
                    f.write(self.result_text.get() + "\n")
                messagebox.showinfo("Експорт завершено", f"Результат записано у файл '{OUTPUT_FILENAME}'")
                self.status_text.set(f"Результат записано у {OUTPUT_FILENAME}")
                self._log_action(f"експортовано результат у файл '{OUTPUT_FILENAME}'")
            except Exception as e_write:
                messagebox.showerror("Помилка запису", f"Не вдалося записати у файл: {e_write}")
                self.status_text.set("Помилка запису")
                self._log_action("помилка запису у файл")
        finally:
            pass

    def _on_close(self):
        self._log_action("додаток закрито")
        self.root.destroy()

if __name__ == "__main__":
    root = tk.Tk()
    app = CalculatorApp(root)
    root.mainloop()
