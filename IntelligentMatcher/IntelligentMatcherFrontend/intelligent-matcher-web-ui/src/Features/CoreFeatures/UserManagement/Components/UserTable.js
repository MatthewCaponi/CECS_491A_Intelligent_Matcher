import Box from '@material-ui/core/Box';
import React, {useState, useEffect} from 'react';
import { makeStyles, ThemeProvider, withStyles } from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import theme from '../../../../Shared/Theme';

const useStyles = makeStyles({
    container: {
      maxHeight: 440
    }
  });

  const StyledTableCell = withStyles((theme) => ({
  head: {
    backgroundColor: theme.palette.common.black,
    color: theme.palette.common.white,
  },
  body: {
    fontSize: 14,
  },
}))(TableCell);
  
function UserTable(props) {

    const classes = useStyles();

    if (props.rows.length === 0) {
        return (
            <h1>No users found</h1>
        )
    }

    return (
        <Box boxShadow={4}>
            <TableContainer component={Paper} className={classes.container}>
                    <Table  stickyHeader aria-label="simple table">
                        <TableHead>
                            <TableRow className={classes.head}>
                                <StyledTableCell>Id</StyledTableCell>
                                <StyledTableCell align="center">Username</StyledTableCell>
                                <StyledTableCell align="center">Email</StyledTableCell>
                                <StyledTableCell align="center">Role</StyledTableCell>
                                <StyledTableCell align="center">Status</StyledTableCell>
                                <StyledTableCell align="center">Creation Date</StyledTableCell>
                                <StyledTableCell align="center">Last Updated</StyledTableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {(props.rows.map(user => (
                                <TableRow key={user.id}>
                                    <TableCell>{user.id}</TableCell>
                                    <TableCell align="center">{user.username}</TableCell>
                                    <TableCell align="center">{user.emailAddress}</TableCell>
                                    <TableCell align="center">{user.accountType}</TableCell>
                                    <TableCell align="center">{user.accountStatus}</TableCell>
                                    <TableCell align="center">{user.creationDate}</TableCell>
                                    <TableCell align="center">{user.updationDate}</TableCell>
                                </TableRow>
                                    )))}
                        </TableBody>
                    </Table>
            </TableContainer>
        </Box>
        
    )
}

export default UserTable;