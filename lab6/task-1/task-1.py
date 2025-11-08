from abc import ABC, abstractmethod
from datetime import datetime
import tkinter as tk
from tkinter import messagebox

class PlantProduct(ABC):
    def __init__(self, name, price, expiration_date):
        self.name = name
        self.price = float(price)
        self.expiration_date = datetime.strptime(expiration_date, "%Y-%m-%d")

    @abstractmethod
    def info(self):
        pass

    @abstractmethod
    def is_expired(self):
        pass

    def days_to_expire(self):
        delta = (self.expiration_date - datetime.now()).days
        return delta

    def base_info(self):
        return f"Name: {self.name}, Price: {self.price} грн, Exp: {self.expiration_date.date()}"


class Fruit(PlantProduct):
    def __init__(self, name, price, expiration_date, sweetness):
        super().__init__(name, price, expiration_date)
        self.sweetness = int(sweetness)

    def info(self):
        return (f"Fruit → {self.base_info()}, "
                f"Sweetness: {self.sweetness}/10")

    def is_expired(self):
        return datetime.now() > self.expiration_date

    def make_juice(self):
        return f"You made juice from {self.name}!"


class Vegetable(PlantProduct):
    def __init__(self, name, price, expiration_date, color):
        super().__init__(name, price, expiration_date)
        self.color = color

    def info(self):
        return (f"Vegetable → {self.base_info()}, "
                f"Color: {self.color}")

    def is_expired(self):
        return datetime.now() > self.expiration_date

    def cook_soup(self):
        return f"{self.name} added to soup!"


class ProductApp:
    def __init__(self, root):
        self.root = root
        root.title("Plant Product Database")
        root.geometry("700x450")

        self.products = []

        tk.Label(root, text="Type (fruit/vegetable):").grid(row=0, column=0, padx=5, pady=5)
        tk.Label(root, text="Name:").grid(row=1, column=0, padx=5, pady=5)
        tk.Label(root, text="Price:").grid(row=2, column=0, padx=5, pady=5)
        tk.Label(root, text="Expiration (YYYY-MM-DD):").grid(row=3, column=0, padx=5, pady=5)
        tk.Label(root, text="Extra (sweetness or color):").grid(row=4, column=0, padx=5, pady=5)

        self.entry_type = tk.Entry(root)
        self.entry_name = tk.Entry(root)
        self.entry_price = tk.Entry(root)
        self.entry_exp = tk.Entry(root)
        self.entry_extra = tk.Entry(root)

        self.entry_type.grid(row=0, column=1)
        self.entry_name.grid(row=1, column=1)
        self.entry_price.grid(row=2, column=1)
        self.entry_exp.grid(row=3, column=1)
        self.entry_extra.grid(row=4, column=1)

        tk.Button(root, text="Add Product", command=self.add_product).grid(row=5, column=0, pady=10)
        tk.Button(root, text="Show All", command=self.show_all).grid(row=5, column=1, pady=10)
        tk.Button(root, text="Find Expired", command=self.find_expired).grid(row=5, column=2, pady=10)
        tk.Button(root, text="Special Action", command=self.special_action).grid(row=5, column=3, pady=10)

        self.text = tk.Text(root, width=85, height=15)
        self.text.grid(row=6, column=0, columnspan=4, padx=10, pady=10)

    def add_product(self):
        p_type = self.entry_type.get().strip().lower()
        name = self.entry_name.get()
        price = self.entry_price.get()
        exp = self.entry_exp.get()
        extra = self.entry_extra.get()

        try:
            if p_type == "fruit":
                prod = Fruit(name, price, exp, extra)
            elif p_type == "vegetable":
                prod = Vegetable(name, price, exp, extra)
            else:
                messagebox.showerror("Error", "Type must be 'fruit' or 'vegetable'")
                return
        except Exception as e:
            messagebox.showerror("Error", f"Invalid input: {e}")
            return

        self.products.append(prod)
        messagebox.showinfo("Added", f"{prod.name} added to database!")
        self.clear_entries()

    def clear_entries(self):
        self.entry_type.delete(0, tk.END)
        self.entry_name.delete(0, tk.END)
        self.entry_price.delete(0, tk.END)
        self.entry_exp.delete(0, tk.END)
        self.entry_extra.delete(0, tk.END)

    def show_all(self):
        self.text.delete(1.0, tk.END)
        if not self.products:
            self.text.insert(tk.END, "No products in database.\n")
        else:
            for p in self.products:
                status = "EXPIRED" if p.is_expired() else "OK"
                self.text.insert(tk.END, f"[{status}] {p.info()}\n")

    def find_expired(self):
        self.text.delete(1.0, tk.END)
        expired = [p for p in self.products if p.is_expired()]
        if not expired:
            self.text.insert(tk.END, "No expired products found.\n")
        else:
            for p in expired:
                self.text.insert(tk.END, f"EXPIRED → {p.info()}\n")

    def special_action(self):
        self.text.delete(1.0, tk.END)
        for p in self.products:
            if isinstance(p, Fruit):
                self.text.insert(tk.END, p.make_juice() + "\n")
            elif isinstance(p, Vegetable):
                self.text.insert(tk.END, p.cook_soup() + "\n")

root = tk.Tk()
app = ProductApp(root)
root.mainloop()