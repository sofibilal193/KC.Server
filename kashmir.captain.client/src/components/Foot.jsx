/* eslint-disable no-unused-vars */
import React from 'react';
import { Typography } from '@mui/material';

function Foot() {
    return (
        <footer style={{ textAlign: 'center', padding: '20px', marginTop: '30px', backgroundColor: '#f1f1f1' }}>
            <Typography variant="body2" color="textSecondary">
                © 2024 Construction Dashboard. All rights reserved.
            </Typography>
        </footer>
    );
}

export default Foot;
