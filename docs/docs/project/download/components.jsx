import React from "react";
import Link from "@docusaurus/Link";

export const version = '1.0.0';
export const downloadPage = 'https://github.com/G-Epitech/DFMY-RType/releases/download';
export const archivePage = 'https://github.com/G-Epitech/DFMY-RType/archive';

export function ArtefactName({name, platform}) {
    const extensions = {
        windows: 'exe',
        debian: 'deb',
        linux: 'tar.gz',
        source: 'zip',
    };
    const extension = extensions[platform];
    return `${name}-${version}.${extension}`;
}

export function CurrentVersion() {
    return version;
}

export function DownloadLink({href, children}) {
    return (
        <Link to={href} target="_blank" style={{paddingBottom: "10px"}}>
            <button className={"button button--md button--primary"}>{children}</button>
        </Link>
    );
}

export function DynamicLink({platform, ArtefactName, children}) {
    const binary = `${downloadPage}/v${version}/${ArtefactName}-${version}`;
    const source = `${archivePage}/tags/v${version}.zip`;

    switch (platform) {
        case 'windows':
            return (
                <DownloadLink href={`${binary}.exe`}>{children}</DownloadLink>
            );
        case 'debian':
            return (
                <DownloadLink href={`${binary}.deb`}>{children}</DownloadLink>
            );
        case 'linux':
            return (
                <DownloadLink href={`${binary}.tar.gz`}>{children}</DownloadLink>
            );
        case 'source':
            return (
                <DownloadLink href={source}>{children}</DownloadLink>
            );
    }
}

export function DynamicClientLink({platform, children}) {
    return (
        <DynamicLink platform={platform} ArtefactName={'R-Type-client'}>{children}</DynamicLink>
    );
}

export function DynamicServerLink({platform, children}) {
    return (
        <DynamicLink platform={platform} ArtefactName={'R-Type-server'}>{children}</DynamicLink>
    );
}

