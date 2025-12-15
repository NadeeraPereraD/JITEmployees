import React from "react";
import { useState } from "react";
import { Box, TextField, Button, Paper, Typography } from "@mui/material";

export default function DepartmentForm() {
  const [dept, setDept] = useState({ code: "", name: "" });
  const [error, setError] = useState(false);
  const [loading, setLoading] = useState(false);

  const submit = () => {};

  return (
    <Paper elevation={3} sx={{ p: 3, maxWidth: "calc(100vw - 240px)" }}>
      <Typography variant="h6" gutterBottom>
        Add Department
      </Typography>

      <Box
        sx={{
          display: "flex",
          gap: 2,
          flexDirection: {
            xs: "column",
            md: "row",
          },
        }}
      >
        <TextField
          label="Department Code"
          value={dept.code}
          error={error && !dept.code}
          helperText={error && !dept.code ? "Department Code is required" : ""}
          onChange={(e) => setDept({ ...dept, code: e.target.value })}
          fullWidth
        />

        <TextField
          label="Department Name"
          value={dept.name}
          error={error && !dept.name}
          helperText={error && !dept.name ? "Department Name is required" : ""}
          onChange={(e) => setDept({ ...dept, name: e.target.value })}
          fullWidth
        />
      </Box>
      <Box sx={{ marginTop: "25px" }}>
        <Button variant="contained" onClick={submit} disabled={loading}>
          {loading ? "Saving..." : "Save"}
        </Button>
      </Box>
    </Paper>
  );
}
