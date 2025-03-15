class Node:
    def __init__(self, number):
        self.number = number
        self.left = None
        self.right = None

class BinarySearchTree:
    def __init__(self):
        self.root = None
    
    def add_element(self, number):
        if self.root is None:
            self.root = Node(number)
        else:
            self.add_helper(self.root, number)

    def add_helper(self, current_box, number):
        if number < current_box.number:
            if current_box.left is None:
                current_box.left = Node(number)
            else:
                self.add_helper(current_box.left, number)
        elif number > current_box.number:
            if current_box.right is None:
                current_box.right = Node(number)
            else:
                self.add_helper(current_box.right, number)
    def find_element(self, number):
        return self._find_helper(self.root, number)

    def _find_helper(self, current_box, number):
        if current_box is None:
            return False
        if number == current_box.number:
            return True
        elif number < current_box.number:
            return self._find_helper(current_box.left, number)
        else:
            return self._find_helper(current_box.right, number)
        
tree = BinarySearchTree()
tree.add_element(10)  
tree.add_element(5)   
tree.add_element(15)  
tree.add_element(3)   
tree.add_element(7)  

print(tree.find_element(7))  
print(tree.find_element(99))
