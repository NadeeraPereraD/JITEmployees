import React from "react";
import { useState, useEffect } from "react";
import { Box, TextField, Button, Paper, Typography } from "@mui/material";

export default function DepartmentForm({
  onSave,
  loading,
  selectedDept,
  clearSelection,
}) {
  const [dept, setDept] = useState({ code: "", name: "" });
  const [error, setError] = useState(false);

  useEffect(() => {
    if (selectedDept) {
      setDept(selectedDept);
    }
  }, [selectedDept]);

  const submit = () => {
    if (!dept.code || !dept.name) {
      setError(true);
      return;
    }

    onSave(dept);
    setDept({ code: "", name: "" });
    setError(false);
    clearSelection();
  };

  return (
    <Paper elevation={3} sx={{ p: 3, maxWidth: "calc(100vw - 240px)" }}>
      <Typography variant="h6" gutterBottom>
        {selectedDept ? "Edit Department" : "Add Department"}
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
          disabled={!!selectedDept}
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
      <Box sx={{ mt: 3 }}>
        <Button variant="contained" onClick={submit} disabled={loading}>
          {loading ? "Saving..." : "Save"}
        </Button>

        {selectedDept && (
          <Button sx={{ ml: 2 }} onClick={clearSelection}>
            Cancel
          </Button>
        )}
      </Box>
    </Paper>
  );
}
