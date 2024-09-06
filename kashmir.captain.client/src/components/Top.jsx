/* eslint-disable no-unused-vars */
import React from 'react';
import { Typography, Container } from '@mui/material';

function Top() {
    return (
        <Container style={{ textAlign: 'center', marginTop: '30px' }}>
            <Typography variant="h4" gutterBottom>
                Welcome to the Construction Dashboard
            </Typography>
            <Typography variant="body1" paragraph>
                Manage and view all resources related to construction.
            </Typography>
        </Container>
    );
}

export default Top;
