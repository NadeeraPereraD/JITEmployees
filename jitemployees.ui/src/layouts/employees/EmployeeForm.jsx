import React, { useState, useEffect } from "react";
import {
  Box,
  TextField,
  Button,
  Paper,
  Typography,
  MenuItem,
  InputLabel,
  FormControl,
  Select,
} from "@mui/material";
import { LocalizationProvider, DatePicker } from "@mui/x-date-pickers";
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFns";

export default function EmployeeForm({
  onSave,
  loading,
  selectedEmp,
  clearSelection,
  departments = [],
}) {
  const [employee, setEmployee] = useState({
    firstName: "",
    lastName: "",
    email: "",
    dob: null,
    age: "",
    salary: "",
    department: "",
  });

  const [error, setError] = useState(false);

  useEffect(() => {
    if (employee.dob) {
      const today = new Date();
      const birthDate = new Date(employee.dob);
      let age = today.getFullYear() - birthDate.getFullYear();
      const m = today.getMonth() - birthDate.getMonth();
      if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
        age--;
      }
      setEmployee((prev) => ({ ...prev, age }));
    } else {
      setEmployee((prev) => ({ ...prev, age: "" }));
    }
  }, [employee.dob]);

  useEffect(() => {
    if (selectedEmp) {
      setEmployee(selectedEmp);
    }
  }, [selectedEmp]);

  useEffect(() => {
    if (selectedEmp) {
      setEmployee({
        ...selectedEmp,
        dob: selectedEmp.dob
          ? new Date(selectedEmp.dob) 
          : null,
      });
    }
  }, [selectedEmp]);

  const submit = () => {
    const newError = {};
    if (!employee.firstName) newError.firstName = true;
    if (!employee.lastName) newError.lastName = true;
    if (!employee.email) newError.email = true;
    if (!employee.dob) newError.dob = true;
    if (!employee.salary) newError.salary = true;
    if (!employee.department) newError.department = true;

    setError(newError);

    if (Object.keys(newError).length > 0) return;

    onSave(employee);
    setEmployee({ firstName: "", lastName: "", email: "", dob: "", salary: "", department: "" });
    setError(false);
    clearSelection();
  };

  return (
    <Paper elevation={3} sx={{ p: 3, maxWidth: "calc(100vw - 240px)" }}>
      <Typography variant="h6" gutterBottom>
        {selectedEmp ? "Edit Employee" : "Add Employee"}
      </Typography>

      <LocalizationProvider dateAdapter={AdapterDateFns}>
        <Box
          sx={{
            display: "flex",
            flexWrap: "wrap",
            gap: 2,
            "& > *": { flex: { xs: "1 1 100%", md: "1 1 calc(50% - 8px)" } },
          }}
        >
          <TextField
            label="First Name"
            value={employee.firstName}
            error={!!error.firstName}
            helperText={error.firstName ? "First Name is required" : ""}
            onChange={(e) =>
              setEmployee({ ...employee, firstName: e.target.value })
            }
            fullWidth
          />

          <TextField
            label="Last Name"
            value={employee.lastName}
            error={!!error.lastName}
            helperText={error.lastName ? "Last Name is required" : ""}
            onChange={(e) =>
              setEmployee({ ...employee, lastName: e.target.value })
            }
            fullWidth
          />

          <TextField
            label="Email"
            type="email"
            value={employee.email}
            error={!!error.email}
            helperText={error.email ? "Email is required" : ""}
            onChange={(e) =>
              setEmployee({ ...employee, email: e.target.value })
            }
            fullWidth
          />

          <DatePicker
            label="Date of Birth"
            value={employee.dob}
            onChange={(newValue) => setEmployee({ ...employee, dob: newValue })}
            renderInput={(params) => (
              <TextField
                {...params}
                error={!!error.dob}
                helperText={error.dob ? "Date of Birth is required" : ""}
                fullWidth
              />
            )}
          />

          <TextField
            label="Age"
            value={employee.age}
            InputProps={{
              readOnly: true,
            }}
            fullWidth
          />

          <TextField
            label="Salary"
            value={employee.salary}
            error={!!error.salary}
            helperText={error.salary ? "Salary is required" : ""}
            onChange={(e) =>
              setEmployee({ ...employee, salary: e.target.value })
            }
            fullWidth
          />

          <FormControl fullWidth error={!!error.department}>
            <InputLabel>Department</InputLabel>
            <Select
              value={employee.department}
              label="Department"
              onChange={(e) =>
                setEmployee({ ...employee, department: e.target.value })
              }
            >
              {departments.map((dept) => (
                <MenuItem key={dept.id} value={dept.name}>
                  {dept.name}
                </MenuItem>
              ))}
            </Select>
            {error.department && (
              <Typography color="error" variant="caption">
                Department is required
              </Typography>
            )}
          </FormControl>
        </Box>
      </LocalizationProvider>

      <Box sx={{ marginTop: 3 }}>
        <Button variant="contained" onClick={submit} disabled={loading}>
          {loading ? "Saving..." : "Save"}
        </Button>
      </Box>
    </Paper>
  );
}
