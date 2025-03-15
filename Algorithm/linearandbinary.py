def BinarySearch(array, x, low, high):
    while low <= high:
        mid = low + (high - low) // 2
        if x == array[mid]:
            return mid
        elif x > array[mid]:
            low = mid + 1
        else:
            high = mid - 1
    return -1

def LinearSearch(array, x):
    for i in range(len(array)):
        if array[i] == x:
            return i
    return -1

array = [1, 2, 3, 4, 5]
x = int(input())

resultbinary = BinarySearch(array, x, 0, len(array) - 1)
resultlinear = LinearSearch(array, x)

if resultbinary != -1:
    print("Binary Search: present")
else:
    print("Binary Search: absent")

if resultlinear != -1:
    print("Linear Search: present")
else:
    print("Linear Search: absent")
