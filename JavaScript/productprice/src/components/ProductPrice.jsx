import React from "react";
import '../styles/ProductPrice.css';

function ProductPrice({ name, price, quantity }) {
  const totalPrice = price * quantity;

    return (
        <div className="product-card">
            <h2 className="product-name">{name}</h2>
            <p className="product-detail">Price: ${price.toFixed(2)}</p>
            <p className="product-detail">Quantity: {quantity}</p>
            <p className="product-total">Total: ${totalPrice.toFixed(2)}</p>
        </div>
    );
}

export default ProductPrice;