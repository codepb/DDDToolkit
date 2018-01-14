import * as React from 'react';

interface BookFormProps {
    onSubmit: React.EventHandler<React.FormEvent<HTMLFormElement>>
    onFieldChange: React.EventHandler<React.ChangeEvent<HTMLInputElement>>
}

const BookForm = ({ onSubmit, onFieldChange } : BookFormProps) => 
    <form onSubmit={onSubmit}>
        <div>
            <label htmlFor="title">Title</label>
            <input type="text" name="title" onChange={onFieldChange} />
        </div>
        <div>
            <label htmlFor="firstName">First Name</label>
            <input type="text" name="firstName" onChange={onFieldChange} />
        </div>
        <div>
            <label htmlFor="lastName">Last Name</label>
            <input type="text" name="lastName" onChange={onFieldChange} />
        </div>
        <div>
            <label htmlFor="isbn">ISBN</label>
            <input type="text" name="isbn" maxLength={13} onChange={onFieldChange} />
        </div>
        <button type="submit">Submit</button>
    </form>;

export default BookForm;
