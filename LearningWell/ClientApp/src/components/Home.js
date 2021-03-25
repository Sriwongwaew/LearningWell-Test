import React, { Component } from 'react';

export class Home extends Component {


    constructor(props) {
        super(props);
        this.state = { forecasts: [], loading: true };
    }

  static displayName = Home.name;

    componentDidMount() {
        this.populateTable();
    }

    static renderTable(participationResult) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Year</th>
                        <th>County</th>
                        <th>Procent</th>
                    </tr>
                </thead>
                <tbody>
                    {participationResult.map(result =>
                        <tr key={result.year}>
                            <td>{result.year}</td>
                            <td>{result.name}</td>
                            <td>{result.value}%</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }
    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Home.renderTable(this.state.participationResult);

        return (
            <div>
                <h1 id="tabelLabel" >Highest participation each year by county</h1>
                {contents}
            </div>
        );
    

    }
    async populateTable() {
        const response = await fetch('ParticipationStatistic');
        const data = await response.json();
        this.setState({ participationResult: data, loading: false });
    }
}
