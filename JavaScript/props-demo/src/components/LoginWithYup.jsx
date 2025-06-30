import React, { useState } from 'react';
import * as Yup from 'yup';

function LoginWithYup() {
  const [formData, setFormData] = useState({ email: '', password: '' });
  const [errors, setErrors] = useState({});
  const [success, setSuccess] = useState('');

  const schema = Yup.object().shape({
    email: Yup.string().email('Enter a valid Email').required('Email is Required'),
    password: Yup.string().min(6, 'Password must be more than 6 chrs').required('Enter password'),
  });

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await schema.validate(formData, { abortEarly: false });
      setErrors({});
      setSuccess('Login successful!');
    } catch (err) {
      const formErrors = {};
      err.inner.forEach((error) => {
        formErrors[error.path] = error.message;
      });
      setErrors(formErrors);
      setSuccess('');
    }
  };

  return (
    <div className="container d-flex justify-content-center align-items-center" style={{ minHeight: '100vh' }}>
      <div className="card p-4 shadow" style={{ maxWidth: '400px', width: '100%' }}>
        <h3 className="text-center mb-3">Login (No Formik)</h3>

        {success && <div className="alert alert-success">{success}</div>}

        <form onSubmit={handleSubmit} noValidate autoComplete="off">
          <div className="mb-3">
            <label className="form-label">Email</label>
            <input
              type="email"
              name="email"
              className={`form-control ${errors.email ? 'is-invalid' : ''}`}
              value={formData.email}
              onChange={handleChange}
            />
            {errors.email && <div className="invalid-feedback">{errors.email}</div>}
          </div>

          <div className="mb-3">
            <label className="form-label">Password</label>
            <input
              type="password"
              name="password"
              className={`form-control ${errors.password ? 'is-invalid' : ''}`}
              value={formData.password}
              onChange={handleChange}
            />
            {errors.password && <div className="invalid-feedback">{errors.password}</div>}
          </div>

          <button type="submit" className="btn btn-primary w-100">Login</button>
        </form>
      </div>
    </div>
  );
}

export default LoginWithYup;
