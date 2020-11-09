import { Button, Menu, MenuItem } from "@material-ui/core";
import { MoreVert } from "@material-ui/icons";
import React, { useState } from "react";
import styled from "styled-components";

const ContextMenuStyle = styled.div`
  button {
    width: 64px;
  }
`;

interface ContextMenuItem {
  title: string;
  action: Function;
}

interface ContextMenuProps {
  items: ContextMenuItem[];
}

const ContextMenu = (props: ContextMenuProps) => {
  const [anchorEl, setAnchorEl] = useState(null);
  return (
    <ContextMenuStyle>
      <Button
        aria-controls="simple-menu"
        aria-haspopup="true"
        onClick={(e) => setAnchorEl(e.currentTarget as any)}
      >
        <MoreVert color="secondary" />
      </Button>
      <Menu
        id="simple-menu"
        anchorEl={anchorEl}
        keepMounted
        open={Boolean(anchorEl)}
        onClose={() => setAnchorEl(null)}
      >
        {props.items.map((x, i) => (
          <MenuItem
            key={i}
            onClick={() => {
              x.action();
              setAnchorEl(null as any);
            }}
          >
            Usuń
          </MenuItem>
        ))}
      </Menu>
    </ContextMenuStyle>
  );
};

export default ContextMenu;
