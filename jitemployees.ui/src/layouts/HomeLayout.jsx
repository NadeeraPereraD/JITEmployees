import React from "react";
import LeftPanel from "../components/common/LeftPanel";
import Typography from "@mui/material/Typography";
import DepartmentForm from "./departments/DepartmentForm";
import DepartmentTable from "./departments/DepartmentTable";
import EmployeeForm from "./employees/EmployeeForm";
import EmployeeTable from "./employees/EmployeeTable";
import DepartmentLayout from "./departments/DepartmentLayout";
import EmployeeLayout from "./employees/EmployeeLayout";

export default function HomeLayout() {
  return (
    <div className="d-flex">
      <LeftPanel />
      <div>
        <Typography
          variant="h6"
          noWrap
          component="div"
          style={{
            padding: "15px",
            height: "75px",
            backgroundColor: "#2e82d6",
            display: "flex",
            alignItems: "center",
            width: "calc(100vw - 240px)",
            fontWeight: "bold",
            fontSize: "25px",
            color: "white"
          }}
        >
          Responsive drawer
        </Typography>
        <div>
            {/* <DepartmentLayout/> */}
            <EmployeeLayout/>
        </div>
      </div>
    </div>
  );
}
