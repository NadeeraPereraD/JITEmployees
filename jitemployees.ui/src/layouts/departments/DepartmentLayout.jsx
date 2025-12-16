import React, { useEffect, useState } from "react";
import DepartmentForm from "./DepartmentForm";
import DepartmentTable from "./DepartmentTable";
import {
  getDepartments,
  createDepartment,
  updateDepartment,
  deleteDepartment,
} from "../../services/DepartmentService";

export default function DepartmentLayout() {
  const [departments, setDepartments] = useState([]);
  const [selectedDept, setSelectedDept] = useState(null);
  const [loading, setLoading] = useState(false);

  // GET
  const loadDepartments = async () => {
    try {
        const data = await getDepartments();

        const mappedData = data.map((d) => ({
            id: d.id,
            code: d.departmentCode,
            name: d.departmentName,
            isActive: d.isActive,
        }));

        setDepartments(mappedData);
    } catch (err) {
        console.error(err);
    }
  };

  useEffect(() => {
    loadDepartments();
  }, []);

  // CREATE or UPDATE
  const handleSave = async (dept) => {
    try {
      setLoading(true);

      if (selectedDept) {
        await updateDepartment(dept);
      } else {
        await createDepartment(dept);
      }

      setSelectedDept(null);
      loadDepartments();
    } catch (err) {
      alert(err.message);
    } finally {
      setLoading(false);
    }
  };

  // EDIT
  const handleEdit = (dept) => {
    setSelectedDept(dept);
  };

  // DELETE
  const handleDelete = async (id) => {
    if (!window.confirm("Are you sure?")) return;

    try {
      await deleteDepartment(id);
      loadDepartments();
    } catch (err) {
      alert(err.message);
    }
  };

  return (
    <>
      <DepartmentForm
        onSave={handleSave}
        loading={loading}
        selectedDept={selectedDept}
        clearSelection={() => setSelectedDept(null)}
      />

      <DepartmentTable
        departments={departments}
        onEdit={handleEdit}
        handleDelete={handleDelete}
      />
    </>
  );
}