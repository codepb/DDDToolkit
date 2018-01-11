import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import BookForm from './BookForm';

declare module 'react' {
    class Fragment extends React.Component { }
}

interface CreateBookProps {

}

interface CreateBookState {
    title: string,
    firstName: string,
    lastName: string,
    isbn: string,
    [key: string]: string
}

class CreateBook extends React.Component<RouteComponentProps<CreateBookProps>, CreateBookState> {
    constructor() {
        super();
        this.state = {
            title: '',
            firstName: '',
            lastName: '',
            isbn: ''
        }
    }

    private handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        const data = {
            title: this.state.title,
            author: {
                firstName: this.state.firstName,
                lastName: this.state.lastName
            },
            isbn: {
                value: this.state.isbn
            }
        };

        fetch('api/Book', {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            method: 'post',
            body: JSON.stringify(data)
        }).then(e => this.props.history.push('/'));
    }

    private handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        var field = e.target.name;
        var value = e.target.value;

        this.setState({ [field]: value });
    }

    public render() {
        return (
            <div>
                <h2>Create Book</h2>
                <BookForm onSubmit={this.handleSubmit} onFieldChange={this.handleChange} />
            </div>);
    }
}

export default CreateBook;
