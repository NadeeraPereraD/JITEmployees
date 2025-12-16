import { Email } from "@mui/icons-material";

const API_URL = "https://localhost:7289/api/employees";

export async function getEmployees() {
    const res = await fetch(API_URL);   
    return res.json();
}

export async function createEmployee(employee, departments) {  
    const selectedDept = departments.find(d=> d.name === employee.department);
    
    const payload = {
        firstName: employee.firstName,
        lastName: employee.lastName,
        email: employee.email,
        dateOfBirth: employee.dob,
        salary: employee.salary,
        departmentName: employee.department,
    };
    console.log(payload)

    const res = await fetch(API_URL, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload)
    });

    if(!res.ok){
        const text = await res.text();
        throw new Error(text);
    }
    return res.json();
}

export async function updateEmployee(employee) { 
    console.log(employee);

    const payload = {
        id: employee.id,
        firstName: employee.firstName,
        lastName: employee.lastName,
        email: employee.email,
        dateOfBirth: employee.dob,
        salary: employee.salary,
        departmentName: employee.department
    };

    const res = await fetch(`${API_URL}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload)
    });
    if (!res.ok) {
        const text = await res.text();
        throw new Error(text || `API request failed: ${res.status}`);
    }
    console.log(res);
    
    const text = await res.text();
    return text ? JSON.parse(text) : { message: "Employee updated successfully" };
}

export async function deleteEmployee(id) {
    const res = await fetch(`${API_URL}`, {
        method: "DELETE",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ id })
    });

    if (!res.ok) {
        const text = await res.text();
        throw new Error(text);
    }
    return res.json();
}