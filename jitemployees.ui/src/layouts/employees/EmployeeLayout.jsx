import React, { useEffect, useState } from "react";
import EmployeeForm from "./EmployeeForm";
import EmployeeTable from "./EmployeeTable";
import { getEmployees, createEmployee, updateEmployee, deleteEmployee } from "../../services/EmployeeService";
import { getDepartments } from "../../services/DepartmentService";

export default function EmployeeLayout() {
    const [departments, setDepartments] = useState([]);
    const [employee, setEmployee] = useState([]);
    const [selectedEmp, setSelectedEmp] = useState(null);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        loadDepartments();
    }, []);

    const loadDepartments = async () => {
        try {
        const data = await getDepartments();

        const mapped = data.map(d => ({
            id: d.id,
            code: d.departmentCode,
            name: d.departmentName,
        }));

        setDepartments(mapped);
        } catch (err) {
        console.error("Failed to load departments", err);
        }
    };

    const loadEmployees = async () => {
        try {
            const data = await getEmployees();
            console.log(data);
            const mappedData = data.map((d) => ({
                id: d.id,
                firstName: d.firstName,
                lastName: d.lastName,
                email: d.email,
                dob: d.dateOfBirth,
                age: d.age,
                salary: d.salary,
                department: d.departmentName,
                isActive: d.isActive,
            }));
    
            setEmployee(mappedData);
        } catch (err) {
            console.error(err);
        }
    };

    useEffect(() => {
        loadEmployees();
    }, []);

    const handleSave = async (employee) => {
        try {
          setLoading(true);
    
          if (selectedEmp) {
            await updateEmployee(employee);
          } else {
            await createEmployee(employee);
          }
    
          setSelectedEmp(null);
          loadEmployees();
        } catch (err) {
          alert(err.message);
        } finally {
          setLoading(false);
        }
    };

    const handleEdit = (dept) => {
        setSelectedEmp(dept);
    };

    const handleDelete = async (id) => {
        if (!window.confirm("Are you sure?")) return;
    
        try {
          await deleteEmployee(id);
          loadEmployees();
        } catch (err) {
          alert(err.message);
        }
    };
    
  return (
    <>
        <EmployeeForm
            onSave={handleSave}
            loading={loading}
            selectedEmp={selectedEmp}
            clearSelection={() => setSelectedEmp(null)}
            departments={departments}
        />

        <EmployeeTable
            employees={employee}
            onEdit={handleEdit}
            handleDelete={handleDelete}
        />
    </>
  )
}
