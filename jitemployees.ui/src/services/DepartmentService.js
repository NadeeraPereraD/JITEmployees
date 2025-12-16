const API_URL = "https://localhost:7289/api/departments";

export async function getDepartments() {
    const res = await fetch(API_URL);   
    return res.json();
}

export async function createDepartment(department) {  
    const payload = {
        departmentCode: department.code,
        departmentName: department.name
    };

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

export async function updateDepartment(department) { 
    console.log(department);

    const payload = {
        id: department.id,
        departmentCode: department.code,
        departmentName: department.name
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
    return text ? JSON.parse(text) : { message: "Department updated successfully" };
}

export async function deleteDepartment(id) {
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