import React from "react";
import DataTable from "../../components/DataTable.jsx"

export default function DepartmentTable({ departments = [], onEdit, handleDelete }) {
  const columns = [
    { field: "code", headerName: "Department Code" },
    { field: "name", headerName: "Department Name" },
  ];

  const actions = [
    { label: "Edit", onClick: onEdit, color: "primary" },
    { label: "Delete", onClick: (row) => handleDelete(row.code), color: "error" },
  ];

  return <DataTable data={departments} columns={columns} actions={actions} />;
}
