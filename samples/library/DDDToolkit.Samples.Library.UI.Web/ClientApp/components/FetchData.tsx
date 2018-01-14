import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface FetchDataExampleState {
    books: Book[];
    loading: boolean;
}

export class FetchData extends React.Component<RouteComponentProps<{}>, FetchDataExampleState> {
    constructor() {
        super();
        this.state = { books: [], loading: true };

        fetch('api/Book')
            .then(response => response.json() as Promise<Book[]>)
            .then(data => {
                this.setState({ books: data, loading: false });
            });
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FetchData.renderBooksTable(this.state.books);

        return <div>
            <h1>Books</h1>
            <p>This component demonstrates fetching data from the server.</p>
            { contents }
        </div>;
    }

    private static renderBooksTable(books: Book[]) {
        return <table className='table'>
            <thead>
                <tr>
                    <th>Title</th>
                    <th>Author</th>
                    <th>ISBN</th>
                </tr>
            </thead>
            <tbody>
            {books.map(book =>
                <tr key={ book.isbn.value }>
                    <td>{ book.title }</td>
                    <td>{ book.author.fullName }</td>
                    <td>{ book.isbn.value }</td>
                </tr>
            )}
            </tbody>
        </table>;
    }
}

interface Book {
    title: string;
    author: Author;
    isbn: ISBN;
}

interface Author {
    firstName: string;
    lastName: string;
    fullName: string;
}

interface ISBN {
    value: string;
}
