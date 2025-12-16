import React from "react";
import DataTable from "../../components/DataTable";

export default function EmployeeTable({
  employees = [],
  onEdit,
  handleDelete,
}) {
  const columns = [
    { field: "firstName", headerName: "First Name" },
    { field: "lastName", headerName: "Last Name" },
    { field: "email", headerName: "Email" },
    {
      field: "dob",
      headerName: "Date of Birth",
      format: (value) => new Date(value).toISOString().split("T")[0],
    },
    { field: "age", headerName: "Age" },
    {
      field: "salary",
      headerName: "Salary",
      format: (value) =>
        Number(value).toLocaleString("en-US", {
          minimumFractionDigits: 2,
          maximumFractionDigits: 2,
        }),
    },
    { field: "department", headerName: "Department" },
  ];

  const actions = [
    { label: "Edit", onClick: onEdit, color: "primary" },
    {
      label: "Delete",
      onClick: (row) => handleDelete(row.id),
      color: "error",
    },
  ];

  return <DataTable data={employees} columns={columns} actions={actions} />;
}
