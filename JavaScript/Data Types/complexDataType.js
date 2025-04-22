let person = {
    name: "Scott",
    age: 30,
    address: {
        city: "Pune",
        zipcode: "42001"
    },
    hobbies: ["reading", "swimming", "sports"],
    greet: function() {
        return `hello, ${this.name}`;
    }
};

console.log(`Name=${person.name} Age=${person.age}
    city=${person.address.city} zipcode=${person.address.zipcode}
    Hobby=${person.hobbies[0]}`);

let myDate=new Date("2025-04-03")
let date1 = new Date();
console.log(myDate);
console.log(date1.getDate())