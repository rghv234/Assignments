const products = [
  {
    id: 1,
    name: "Laptop",
    price: 1000,
    category: "Electronics",
    inStock: true,
    details: {
      manufacturer: "ABC Corp",
      warranty: "2 years"
    }
  },
  {
    id: 2,
    name: "Smartphone",
    price: 800,
    category: "Electronics",
    inStock: false,
    details: {
      manufacturer: "XYZ Inc",
      warranty: "1 year"
    }
  }
    ,
    {
        id: 3,
        name: "Coffee Maker",
        price: 150,
        category: "Home Appliances",
        inStock: true,
        details: {
        manufacturer: "HomeTech",
        warranty: "6 months"
        }
    },
    {
        id: 4,
        name: "Blender",
        price: 100,
        category: "Home Appliances",
        inStock: true,
        details: {
        manufacturer: "KitchenPro",
        warranty: "1 year"
        }
    },
    {
        id: 5,
        name: "Headphones",
        price: 200,
        category: "Electronics",
        inStock: true,
        details: {
        manufacturer: "AudioMax",
        warranty: "1 year"
        }
    }
];

const inStockProducts = products.filter(product => product.inStock);
console.log("In-stock products:");
inStockProducts.forEach(product => {
    console.log("- " + product.name);
});

const topRatedProducts = products.filter(product => product.price > 100);
console.log("Top-rated products (price > 100):");
topRatedProducts.forEach(product => {
    console.log("- " + product.name);
});