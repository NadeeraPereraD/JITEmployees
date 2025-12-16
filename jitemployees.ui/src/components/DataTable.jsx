import React, { useState } from "react";
import {
  Box,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Button,
  TextField,
  Typography,
} from "@mui/material";

export default function DataTable({ data = [], columns = [], actions = [] }) {
  const [search, setSearch] = useState("");

  const filteredData = data.filter((row) =>
    columns.some((col) => {
      const value = row[col.field];
      return (
        value && value.toString().toLowerCase().includes(search.toLowerCase())
      );
    })
  );

  const formatDate = (value) => {
    if (!value) return "";
    return new Date(value).toISOString().split("T")[0];
  };

  const formatCurrency = (value) => {
    if (value == null) return "";
    return Number(value).toLocaleString("en-US", {
      minimumFractionDigits: 2,
      maximumFractionDigits: 2,
    });
  };

  return (
    <Box sx={{ p: 3 }}>
      <Box sx={{ mb: 2 }}>
        <TextField
          fullWidth
          variant="outlined"
          label="Search..."
          value={search}
          onChange={(e) => setSearch(e.target.value)}
        />
      </Box>

      <TableContainer component={Paper}>
        <Table>
          <TableHead sx={{ bgcolor: "primary.main" }}>
            <TableRow>
              {columns.map((col) => (
                <TableCell key={col.field} sx={{ color: "white" }}>
                  {col.headerName}
                </TableCell>
              ))}
              {actions.length > 0 && (
                <TableCell sx={{ color: "white" }}>Actions</TableCell>
              )}
            </TableRow>
          </TableHead>

          <TableBody>
            {filteredData.length > 0 ? (
              filteredData.map((row, idx) => (
                <TableRow key={row.id || idx}>
                  {columns.map((col) => (
                    <TableCell key={col.field}>
                      {col.format
                        ? col.format(row[col.field], row)
                        : row[col.field]}
                    </TableCell>
                  ))}

                  {actions.length > 0 && (
                    <TableCell>
                      {actions.map((action, index) => (
                        <Button
                          key={index}
                          size="small"
                          variant={action.variant || "contained"}
                          color={action.color || "primary"}
                          sx={{ mr: 1 }}
                          onClick={() => action.onClick(row)}
                        >
                          {action.label}
                        </Button>
                      ))}
                    </TableCell>
                  )}
                </TableRow>
              ))
            ) : (
              <TableRow>
                <TableCell colSpan={columns.length + 1} align="center">
                  <Typography>No records found</Typography>
                </TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </TableContainer>
    </Box>
  );
}
