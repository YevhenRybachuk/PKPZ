import random


def check_hit(x, y, R):
    if x >= 0 and y >= 0 and x * x + y * y <= R * R:
        return True

    if x <= 0 and y <= 0 and x + y >= -R:
        return True

    return False


def generate_shots(n, low, high):
    shots = []
    for _ in range(n):
        x = random.uniform(low, high)
        y = random.uniform(low, high)
        shots.append((round(x, 2), round(y, 2)))
    return shots


def main():
    R = float(input("Enter R: "))
    shots = generate_shots(10, -10, 10)

    print("\nShot №\tCoordinates\t\tResult")
    for i, (x, y) in enumerate(shots, start=1):
        if check_hit(x, y, R):
            result = "Hit the target"
        else:
            result = "Missed the target"
        print(f"{i}\t({x}, {y})\t\t{result}")


if __name__ == "__main__":
    main()