import tkinter as tk
from tkinter import messagebox

class Transport:
    def __init__(self, name="Unknown", speed=0, capacity=0):
        self.name = name
        self.speed = speed
        self.capacity = capacity

    def __del__(self):
        print("Лабораторна робота виконана студентом 2 курсу Рибачуком Євгеном")

    def move(self):
        return f"{self.name} moves at {self.speed} km/h."

    def Show(self):
        return f"Transport: {self.name}\nSpeed: {self.speed} km/h\nCapacity: {self.capacity} passengers"


class Car(Transport):
    def __init__(self, name="Car", speed=0, capacity=0, fuel_type="Petrol", doors=4, brand="Generic"):
        super().__init__(name, speed, capacity)
        self.fuel_type = fuel_type
        self.doors = doors
        self.brand = brand

    def move(self):
        return f"The car {self.brand} drives at {self.speed} km/h."

    def Show(self):
        return (f"Car: {self.brand}\nName: {self.name}\nSpeed: {self.speed} km/h\n"
                f"Capacity: {self.capacity}\nFuel: {self.fuel_type}\nDoors: {self.doors}")


class Train(Transport):
    def __init__(self, name="Train", speed=0, capacity=0, wagons=0, route="Unknown", company="UkrRail"):
        super().__init__(name, speed, capacity)
        self.wagons = wagons
        self.route = route
        self.company = company

    def move(self):
        return f"The train {self.name} runs on the {self.route} route at {self.speed} km/h."

    def Show(self):
        return (f"Train: {self.name}\nSpeed: {self.speed} km/h\nCapacity: {self.capacity}\n"
                f"Wagons: {self.wagons}\nRoute: {self.route}\nCompany: {self.company}")


class Express(Train):
    def __init__(self, name="Express", speed=0, capacity=0, wagons=0, route="Unknown",
                 company="UkrRail", service_class="First", ticket_price=0):
        super().__init__(name, speed, capacity, wagons, route, company)
        self.service_class = service_class
        self.ticket_price = ticket_price

    def move(self):
        return f"The express {self.name} rushes at {self.speed} km/h with {self.service_class} class."

    def Show(self):
        return (f"Express: {self.name}\nSpeed: {self.speed} km/h\nCapacity: {self.capacity}\n"
                f"Wagons: {self.wagons}\nRoute: {self.route}\nCompany: {self.company}\n"
                f"Service Class: {self.service_class}\nTicket Price: {self.ticket_price} UAH")


class TransportApp:
    def __init__(self, root):
        self.root = root
        self.root.title("Transport Information")
        self.root.geometry("420x540")

        tk.Label(root, text="Choose type of transport:", font=("Arial", 11, "bold")).pack(pady=5)

        self.type_var = tk.StringVar(value="Car")
        options = ["Car", "Train", "Express"]
        self.option_menu = tk.OptionMenu(root, self.type_var, *options, command=self.update_labels)
        self.option_menu.pack()

        self.entries = {}
        self.labels = {}

        self.create_field("Name")
        self.create_field("Speed")
        self.create_field("Capacity")
        self.create_field("Extra1")
        self.create_field("Extra2")
        self.create_field("Extra3")

        self.update_labels("Car")

        tk.Button(root, text="Create Object", command=self.create_object, bg="#d1ffd1").pack(pady=8)
        tk.Button(root, text="Show Info", command=self.show_info, bg="#d1e0ff").pack(pady=4)
        tk.Button(root, text="Show Movement", command=self.show_move, bg="#ffe1d1").pack(pady=4)

        self.output = tk.Text(root, height=10, width=50, font=("Courier", 10))
        self.output.pack(pady=10)

        self.obj = None

    def create_field(self, field_name):
        frame = tk.Frame(self.root)
        frame.pack(pady=3)
        label = tk.Label(frame, text=f"{field_name}:", width=18, anchor="w")
        label.pack(side=tk.LEFT)
        entry = tk.Entry(frame, width=25)
        entry.pack(side=tk.LEFT)
        self.entries[field_name] = entry
        self.labels[field_name] = label

    def update_labels(self, choice):
        if choice == "Car":
            self.labels["Extra1"].config(text="Fuel type:")
            self.labels["Extra2"].config(text="Doors:")
            self.labels["Extra3"].config(text="Brand:")
        elif choice == "Train":
            self.labels["Extra1"].config(text="Wagons:")
            self.labels["Extra2"].config(text="Route:")
            self.labels["Extra3"].config(text="Company:")
        elif choice == "Express":
            self.labels["Extra1"].config(text="Wagons:")
            self.labels["Extra2"].config(text="Route:")
            self.labels["Extra3"].config(text="Company:")

    def create_object(self):
        t = self.type_var.get()
        name = self.entries["Name"].get()
        speed = float(self.entries["Speed"].get() or 0)
        capacity = int(self.entries["Capacity"].get() or 0)
        e1 = self.entries["Extra1"].get()
        e2 = self.entries["Extra2"].get()
        e3 = self.entries["Extra3"].get()

        if t == "Car":
            self.obj = Car(name, speed, capacity, e1 or "Petrol", int(e2 or 4), e3 or "Toyota")
        elif t == "Train":
            self.obj = Train(name, speed, capacity, int(e1 or 10), e2 or "Kyiv-Lviv", e3 or "UkrRail")
        elif t == "Express":
            self.obj = Express(name, speed, capacity, int(e1 or 8), e2 or "Kyiv-Odesa",
                               e3 or "UkrRail", "Business", 800)
        self.output.delete(1.0, tk.END)
        self.output.insert(tk.END, f"{t} created successfully!\n")

    def show_info(self):
        if self.obj:
            self.output.delete(1.0, tk.END)
            self.output.insert(tk.END, self.obj.Show())
        else:
            messagebox.showerror("Error", "No object created!")

    def show_move(self):
        if self.obj:
            messagebox.showinfo("Movement", self.obj.move())
        else:
            messagebox.showerror("Error", "No object created!")


if __name__ == "__main__":
    root = tk.Tk()
    app = TransportApp(root)
    root.mainloop()
