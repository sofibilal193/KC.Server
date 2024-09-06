import { Grid, Paper, Typography } from '@mui/material';

function Body() {
    return (
        <Grid container spacing={3}>
            <Grid item xs={12} md={4}>
                <Paper style={{ padding: '20px', textAlign: 'center' }}>
                    <Typography variant="h5" gutterBottom>
                        Workers
                    </Typography>
                    <Typography variant="body1">
                        Manage and view all workers here.
                    </Typography>
                </Paper>
            </Grid>
            <Grid item xs={12} md={4}>
                <Paper style={{ padding: '20px', textAlign: 'center' }}>
                    <Typography variant="h5" gutterBottom>
                        Tools/Equipments
                    </Typography>
                    <Typography variant="body1">
                        Manage and view all tools and equipment here.
                    </Typography>
                </Paper>
            </Grid>
            <Grid item xs={12} md={4}>
                <Paper style={{ padding: '20px', textAlign: 'center' }}>
                    <Typography variant="h5" gutterBottom>
                        Builders
                    </Typography>
                    <Typography variant="body1">
                        Manage and view all builders here.
                    </Typography>
                </Paper>
            </Grid>
        </Grid>
    );
}

export default Body;
