def input_array():
    n = int(input("Enter size of array: "))
    arr = []
    for i in range(n):
        value = int(input(f"Enter element {i+1}: "))
        arr.append(value)
    return arr


def process_array(arr):
    pos_indices = [i for i, val in enumerate(arr) if val >= 0]


    left = 0
    right = len(pos_indices) - 1
    while left < right:
        i = pos_indices[left]
        j = pos_indices[right]
        arr[i], arr[j] = arr[j], arr[i]
        left += 1
        right -= 1

    return arr


def output_array(arr):
    print("Resulting array:", arr)


def main():
    arr = input_array()
    arr = process_array(arr)
    output_array(arr)


if __name__ == "__main__":
    main()
