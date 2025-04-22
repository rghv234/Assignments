function greet(name, callback){

    //This code will execute before callback function
    console.log(`Hello, ${name}`)

    //code to be executed as the callback
    callback();
}

//code to be executed as the callback

function sayGoodbye(){
    console.log("Goodbye!");
}

//calling the greet function with callback
greet("scott", sayGoodbye)