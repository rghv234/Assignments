string = input()
stack = []
brackets = {
        '(': ')',
        '[': ']',
        '{': '}'
    }
for char in string:
    if char in brackets:
        stack.append(char)
    elif char in brackets.values():
        if not stack or brackets[stack.pop()] != char:
            print("false")
            exit()
if len(stack) == 0:
    print("true")
else:
    print("false")
