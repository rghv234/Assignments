let y = 10;
let x = 5;

let z = x + y;
console.log("The value of z is:", z);

const name = "John";
console.log("Hello, " + name + "!");

let fruits = ["apple", "banana", "cherry"];
fruits.forEach(fruit => {
    console.log("Fruit: " + fruit);
});
fruits.forEach(element => {
    console.log("Element: " + element);
});

appleFruits =["RedApple","GreenApple", "WaterApple"];
let alfruits = fruits.concat(appleFruits);
console.log("After concatenating appleFruits:\n.");
alfruits.forEach(function (fruit) {
    console.log("Fruit: " + fruit);
});

//indexOf
console.log("Index of 'Pineapple':"+allfruits.indexOf("Pineapple"));

//includes ()
console.log("Does the array include 'Mango'?"+ allfruits.includes("Mango"));

//join()
console.log("Joined fruits: "+ allfruits.join(","));

fruits.unshift("Orange");
console.log("After unshift, fruits array: " + fruits);  
fruits.forEach(function (fruit) {
    console.log("Fruit: " + fruit);
});

fruits.shift();
console.log("After shift, fruits array: " + fruits);    
fruits.forEach(function (fruit) {
    console.log("Fruit: " + fruit);
});

fruits.push("Grapes");
fruits.push("Pineapple");
fruits.push("Mango");
console.log("After push, fruits array: " + fruits); 
fruits.forEach(function (fruit) {
    console.log("Fruit: " + fruit);
});

fruits.splice(1, 2, "Kiwi", "Peach");
console.log("After splice, fruits array: " + fruits);   
fruits.forEach(function (fruit) {
    console.log("Fruit: " + fruit);
});

appleFruits = ["RedApple", "GreenApple", "WaterApple"];
let allfruits = fruits.concat(appleFruits); 
console.log("After concatenating appleFruits:\n.");
allfruits.forEach(function (fruit) {
    console.log("Fruit: " + fruit);
});
