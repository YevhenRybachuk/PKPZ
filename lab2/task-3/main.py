import random

def generate_salaries(rows=10, cols=12, low=5000, high=20000):
    salaries = []
    for i in range(rows):
        row = [random.randint(low, high) for _ in range(cols)]
        salaries.append(row)
    return salaries


def print_salaries(salaries):
    print("Salaries matrix (10 persons × 12 months):")
    for i, row in enumerate(salaries, start=1):
        print(f"Person {i:2d}: {row}")


def average_salary(salaries, person_index):
    row = salaries[person_index]
    return sum(row) / len(row)


def main():
    salaries = generate_salaries()

    print_salaries(salaries)

    person = int(input("\nEnter person number (1-10): ")) - 1

    if 0 <= person < len(salaries):
        avg = average_salary(salaries, person)
        print(f"Average yearly salary for person {person+1}: {avg:.2f}")
    else:
        print("Invalid person number!")


if __name__ == "__main__":
    main()
