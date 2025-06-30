import React, { useState } from 'react';
import { useFormik } from 'formik';
import * as Yup from 'yup';

export default function LoginFormik() {
  const [successMessage, setSuccessMessage] = useState('');

  const formik = useFormik({
    initialValues: {
      email: '',
      password: '',
    },
    validationSchema: Yup.object({
      email: Yup.string()
        .email('Enter a valid Email')
        .required('Email is Required'),
      password: Yup.string()
        .required('Enter password')
        .min(6, 'Password must be more than 6 characters'),
    }),
    onSubmit: (values, { resetForm }) => {
      alert('Login success');
      console.log('Form submitted', values);
      setSuccessMessage('Login successful!');
      resetForm();
    },
  });

  return (
    <div className="container d-flex justify-content-center align-items-center" style={{ minHeight: '100vh' }}>
      <div className="card p-4 shadow" style={{ width: '100%', maxWidth: '400px' }}>
        <h3 className="text-center mb-3">Formik Login</h3>

        {successMessage && <div className="alert alert-success">{successMessage}</div>}

        <form onSubmit={formik.handleSubmit} autoComplete="off">
          <div className="mb-3">
            <label className="form-label">Email</label>
            <input
              type="email"
              name="email"
              className={`form-control ${formik.touched.email && formik.errors.email ? 'is-invalid' : ''}`}
              onChange={formik.handleChange}
              onBlur={formik.handleBlur}
              value={formik.values.email}
            />
            {formik.touched.email && formik.errors.email && (
              <div className="invalid-feedback">{formik.errors.email}</div>
            )}
          </div>

          <div className="mb-3">
            <label className="form-label">Password</label>
            <input
              type="password"
              name="password"
              className={`form-control ${formik.touched.password && formik.errors.password ? 'is-invalid' : ''}`}
              onChange={formik.handleChange}
              onBlur={formik.handleBlur}
              value={formik.values.password}
            />
            {formik.touched.password && formik.errors.password && (
              <div className="invalid-feedback">{formik.errors.password}</div>
            )}
          </div>

          <button type="submit" className="btn btn-primary w-100">
            Login
          </button>
        </form>
      </div>
    </div>
  );
}
