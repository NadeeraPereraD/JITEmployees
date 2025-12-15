import React from 'react';
import DataTable from '../../components/DataTable';

export default function EmployeeTable({ employees = [], onEdit, handleDelete }) {
  const columns = [
    { field: "firstName", headerName: "First Name" },
    { field: "lastName", headerName: "Last Name" },
    { field: "email", headerName: "Email" },
    { field: "dob", headerName: "Date of Birth" },
    { field: "age", headerName: "Age" },
    { field: "salary", headerName: "Salary" },
    { field: "department", headerName: "Department" },
  ];

  const actions = [
    { label: "Edit", onClick: onEdit, color: "primary" },
    { label: "Delete", onClick: (row) => handleDelete(row.email), color: "error" },
  ];

  return <DataTable data={employees} columns={columns} actions={actions} />;
}
