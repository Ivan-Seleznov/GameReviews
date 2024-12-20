import { createGlobalStyle } from "styled-components";
import { reset } from "./reset";

export const GlobalStyles = createGlobalStyle`
    ${reset}
    * {
        font-family: sans-serif;
        font-weight: normal;
    }

    a {
        text-decoration: none; 
        color: inherit;
        cursor: pointer;
    }
    
    a:hover,
    a:focus {
        text-decoration: none; 
        outline: none;            
    }
    
    a:active {
        opacity: 0.8; 
    }
`;
