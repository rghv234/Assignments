import React from "react";
import ProductPrice from "./components/ProductPrice";

function App() {
  return (
    <div>
      <ProductPrice name="Laptop" price={999.99} quantity={2} />
      <ProductPrice name="Smartphone" price={499.99} quantity={3} />
      <ProductPrice name="Tablet" price={299.99} quantity={1} />
      <ProductPrice name="Smartwatch" price={199.99} quantity={4} />
      <ProductPrice name="Headphones" price={89.99} quantity={5} />
      <ProductPrice name="Bluetooth Speaker" price={59.99} quantity={2} />
      <ProductPrice name="Wireless Mouse" price={29.99} quantity={6} />
      <ProductPrice name="Keyboard" price={49.99} quantity={3} />
      <ProductPrice name="Monitor" price={199.99} quantity={1} />
      <ProductPrice name="External Hard Drive" price={89.99} quantity={2} />
      <ProductPrice name="USB Flash Drive" price={19.99} quantity={10} />
      <ProductPrice name="Webcam" price={79.99} quantity={3} />
    </div>
  );
}

export default App;
